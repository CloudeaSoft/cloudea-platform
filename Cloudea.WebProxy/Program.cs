// See https://aka.ms/new-console-template for more information
using System.Text.Encodings.Web;

Console.WriteLine("Hello, World!");
string token = "AaKWfqagnoRWTgB2BaTls5Uul/8ZirwWAOmqA3heuOAIVrRIatIHY2jFXdgGKcnRtDJFrhXBhw+E/xDh6XWDjSg0Db8ianAILSnFS3zTxqJKk2808lQJEM7UHuEuF2QxPlHtIIt+qidbkqizJTDvwh0qYNGVwm5TNq7Ojn11+XShpkgHtNI4wEfVPKzRqmww6mtwOVdAC9NAwbD/91jRXwz0zmVo7YLDFa26KuxriSK/E9d0HgMfoYWWtYwL+4XGsNYw+SqinR/F3H6Yu3H5oQZjqOutnbihrcO9uA==";
var a = UrlEncoder.Create();
var res = a.Encode(token);
Console.WriteLine(res);
string token1 = "AaKWfqagnoRWTgB2BaTls5Uul%2F8ZirwWAOmqA3heuOAIVrRIatIHY2jFXdgGKcnRtDJFrhXBhw%2BE%2FxDh6XWDjSg0Db8ianAILSnFS3zTxqJKk2808lQJEM7UHuEuF2QxPlHtIIt%2BqidbkqizJTDvwh0qYNGVwm5TNq7Ojn11%2BXShpkgHtNI4wEfVPKzRqmww6mtwOVdAC9NAwbD%2F91jRXwz0zmVo7YLDFa26KuxriSK%2FE9d0HgMfoYWWtYwL%2B4XGsNYw%2BSqinR%2FF3H6Yu3H5oQZjqOutnbihrcO9uA%3D%3D";
Console.WriteLine(res == token1);