using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Task1
{
    public sealed class Polinome
    {
        private readonly double[] coefficient;
        private static double epsilon;
        private int Degree { get;}

        public Polinome(params double[] coeff)
        {
            if(coeff==null)
                throw new ArgumentNullException();
            coefficient = new double[coeff.Length];
            Array.Copy(coeff,coefficient,coeff.Length);
            Degree = coefficient.Length-1;
            epsilon = double.Parse(ConfigurationManager.AppSettings["epsilon"]);
        }

        public double this[int number]
        {
            get
            {
                if(number>Degree)
                    throw new IndexOutOfRangeException();
                return coefficient[number];
            }
            set
            {
                if (number >= 0 && number < Degree+1) coefficient[number] = value;
                else throw new IndexOutOfRangeException();

            }
        }

        public override bool Equals(object obj)
        {
            var polinom = obj as Polinome;
            return polinom != null && Equals(polinom);
        }

        public bool Equals(Polinome  obj)
        {
            if (ReferenceEquals(this, obj) == true)
                return true;
            if (ReferenceEquals(null, obj) == true)
                return false;
            if (this.coefficient.Length!= obj.coefficient.Length)
                return false;
            for (int i=0;i<obj.coefficient.Length;i++)
                if (this.coefficient[i] != obj.coefficient[i])
                    return false;
            return true;
        }

        public static bool operator==(Polinome obj1, Polinome obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(Polinome obj1, Polinome obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            return coefficient?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            if (coefficient.Length==0) return s.ToString();
            if (coefficient[0] != 0) s.Append($"{coefficient[0]}");
            if (Degree > 0)
            {
                if (coefficient[1] != 0) s.Append($"{+coefficient[1]}x");
                for (int i = 2; i < coefficient.Length; i++)
                {
                    if (coefficient[i] != 0)
                    {
                        if (coefficient[i] > 0) s.Append($"+{coefficient[i]}x^{i}");
                        else s.Append($"{coefficient[i]}x^{i}");
                    }
                }
            }
            return s.ToString();
        }

        public static Polinome operator -(Polinome obj)
        {
            if(obj==null)
                throw new ArgumentNullException();
            Polinome polinome=new Polinome(obj.coefficient);
            for (int i = 0; i < obj.coefficient.Length; i++)
                polinome.coefficient[i] *= -1;
            return polinome;
        }

        public static Polinome operator+(Polinome obj1,Polinome obj2)
        {
            if(ReferenceEquals(obj1,null) || ReferenceEquals(obj2,null))
                throw new ArgumentNullException();

            int minLength = obj1.Degree.CompareTo(obj2.Degree) <= 0 ? obj1.Degree : obj2.Degree;

            Polinome resultObj = obj1.Degree.CompareTo(obj2.Degree) <= 0
                ? new Polinome(obj2.coefficient)
                : new Polinome(obj1.coefficient);
            for (int i = 0; i <= minLength; i++)
                resultObj[i] += obj1.Degree.CompareTo(obj2.Degree) <= 0
                    ? obj1.coefficient[i]
                    : obj2.coefficient[i];
            return resultObj;
        }

        public static Polinome operator -(Polinome obj1, Polinome obj2)
        {
            return obj1 + (-obj2);
        }

    }
}
