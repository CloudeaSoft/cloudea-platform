using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.MiniTest
{
    public class AR
    {

    }

    public class Balance : Entity<PhoneNumber>
    {

    }

    #region Base Classes
    public interface Serializable { }

    public interface Identifier : Serializable { }

    public interface Entity<T> where T : Identifier { }

    public class PhoneNumber : Identifier { }

    public class Red
    {
        private string desc;
        private string rgb;
    }

    public interface AggregateRoot<T> : Entity<T> where T : Identifier { }

    public class WeChatAccount : AggregateRoot<PhoneNumber> { }
    #endregion
}
