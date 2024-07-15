using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Utilities.Concretes
{
    public class DataResult<T> : Result, IDataResult<T> where T : class
    {
        public T? Data { get;}

        public DataResult(T data,bool isSuccess): base(isSuccess) // kalıtım aldığı yeri temsil eder - base  // this - bulundugu classı temsil eder
        {
            Data = data;
        }
        public DataResult(T data,bool isSuccess,string message):base(isSuccess,message) 
        {
            Data = data;
        }

    }
}
