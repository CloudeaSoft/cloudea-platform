using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Data;

namespace Cloudea.MLTest
{
    internal class Program
    {
        private const string a = "Amazon0302.txt";
        private const string b = "view_data.csv";

        //1. Do remember to replace amazon0302.txt with dataset from https://snap.stanford.edu/data/amazon0302.html
        //2. Replace column names with ProductID and CoPurchaseProductID. It should look like this:
        //   ProductID	CoPurchaseProductID
        //   0	1
        //   0  2
        private static string BaseDataSetRelativePath = @"../../../Data";
        private static string TrainingDataRelativePath = $"{BaseDataSetRelativePath}/{b}";
        private static string TrainingDataLocation = GetAbsolutePath(TrainingDataRelativePath);

        private static string BaseModelRelativePath = @"../../../Model";
        private static string ModelRelativePath = $"{BaseModelRelativePath}/model.zip";
        private static string ModelPath = GetAbsolutePath(ModelRelativePath);

        public static void Main(string[] args)
        {
            //STEP 1: Create MLContext to be shared across the model creation workflow objects 
            MLContext mlContext = new MLContext();

            //STEP 2: Read the trained data using TextLoader by defining the schema for reading the product co-purchase dataset
            //        Do remember to replace amazon0302.txt with dataset from https://snap.stanford.edu/data/amazon0302.html
            var traindata = mlContext.Data.LoadFromTextFile(path: TrainingDataLocation,
                                                      columns: new[]
                                                                {
                                                                    new TextLoader.Column("Label", DataKind.Single, 0),
                                                                    new TextLoader.Column(name:nameof(ProductEntry.ProductID), dataKind:DataKind.String, source: new [] { new TextLoader.Range(0) }),
                                                                    new TextLoader.Column(name:nameof(ProductEntry.CoPurchaseProductID), dataKind:DataKind.UInt32, source: new [] { new TextLoader.Range(1) })
                                                                },
                                                      hasHeader: true,
                                                      separatorChar: ',');

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("ProductIDKey", "ProductID")
                .Append(mlContext.Transforms.Conversion.MapValueToKey("CoPurchaseProductIDKey", "CoPurchaseProductID"));
            IDataView transformedData = pipeline.Fit(traindata).Transform(traindata);
            var preview = mlContext.Data.CreateEnumerable<ProductEntry>(transformedData, reuseRowObject: false);
            if (!preview.Any()) {
                Console.WriteLine("没有加载到任何有效的数据实例！");
                return;
            }

            //STEP 3: Your data is already encoded so all you need to do is specify options for MatrxiFactorizationTrainer with a few extra hyperparameters
            //        LossFunction, Alpa, Lambda and a few others like K and C as shown below and call the trainer. 
            MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options();
            options.MatrixColumnIndexColumnName = "ProductIDKey";
            options.MatrixRowIndexColumnName = "CoPurchaseProductIDKey";
            options.LabelColumnName = "Label";
            options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
            options.Alpha = 0.01;
            options.Lambda = 0.025;
            // For better results use the following parameters
            //options.K = 100;
            //options.C = 0.00001;

            //Step 4: Call the MatrixFactorization trainer by passing options.
            var est = mlContext.Recommendation().Trainers.MatrixFactorization(options);
            //STEP 5: Train the model fitting to the DataSet
            //Please add Amazon0302.txt dataset from https://snap.stanford.edu/data/amazon0302.html to Data folder if FileNotFoundException is thrown.
            ITransformer model = est.Fit(transformedData);

            //STEP 6: Create prediction engine and predict the score for Product 63 being co-purchased with Product 3.
            //        The higher the score the higher the probability for this particular productID being co-purchased 
            var predictionengine = mlContext.Model.CreatePredictionEngine<ProductEntry, Copurchase_prediction>(model);
            var prediction = predictionengine.Predict(
                new ProductEntry() {
                    ProductID = "",
                    CoPurchaseProductID = 63
                });

            Console.WriteLine("\n For ProductID = 3 and  CoPurchaseProductID = 63 the predicted score is " + Math.Round(prediction.Score, 1));
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        public static string GetAbsolutePath(string relativeDatasetPath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativeDatasetPath);

            return fullPath;
        }

        public class Copurchase_prediction
        {
            public float Score { get; set; }
        }

        public class ProductEntry
        {
            public string ProductID { get; set; }

            public float CoPurchaseProductID { get; set; }
        }
    }
}
