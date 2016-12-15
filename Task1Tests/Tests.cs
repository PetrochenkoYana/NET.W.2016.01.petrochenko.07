using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Task1;

namespace Task1Tests
{
    [TestFixture]
    public class Tests
    {
       
        #region Test method: public override bool Equals(object o)

        public static IEnumerable<TestCaseData> TestCasesForEquals
        {
            get
            {
                Polinome Polinome = new Polinome(1, 0, 3, 4, 0, 5);
                yield return new TestCaseData(Polinome, Polinome).Returns(true);
                Polinome Polinome2 = new Polinome(1, 0, 3, 4, 0, 5);
                yield return new TestCaseData(Polinome, Polinome2).Returns(true);
                yield return new TestCaseData(Polinome, null).Returns(false);
                Polinome2 = new Polinome(1, 0, 3, 4, 0);
                yield return new TestCaseData(Polinome, Polinome2).Returns(false);
                yield return new TestCaseData(Polinome, new double[] { 1, 0, 3, 4, 0, 5 }).Returns(false);

            }
        }
        

        [Test, TestCaseSource(nameof(TestCasesForEquals))]
        public bool TestEquals(object obj1, object obj2)
        {
            return ((Polinome)obj1).Equals(obj2 as Polinome);
        }

        #endregion

        #region Test method: public override string ToString()
        public static IEnumerable<TestCaseData> TestCasesForToString
        {
            get
            {
                Polinome Polinome = new Polinome(1, 0, 3, 4, 0, 5);
                yield return new TestCaseData(Polinome).Returns("1+3x^2+4x^3+5x^5");
                Polinome = new Polinome(-1, 0, 3, -4, 0, 5);
                yield return new TestCaseData(Polinome).Returns("-1+3x^2-4x^3+5x^5");
                double[] coefs = { 0, 1, 2, 0, 0, 0 };
                Polinome = new Polinome(coefs);
                yield return new TestCaseData(Polinome).Returns("1x+2x^2");
                Polinome = new Polinome(0);
                yield return new TestCaseData(Polinome).Returns("");

            }
        }

        [Test, TestCaseSource(nameof(TestCasesForToString))]
        public string TestToString(Polinome Polinome)
        {
            return Polinome.ToString();

        }
        #endregion

       #region Test method: public static Polinome operator+(Polinome lhs, Polinome rhs)
        public static IEnumerable<TestCaseData> TestCasesForAdditing
        {
            get
            {
                yield return new TestCaseData(new Polinome(1, 2, 3), new Polinome(2, 3, 4, 5)).Returns(new Polinome(3, 5, 7, 5));
                yield return new TestCaseData(new Polinome(1, 2, 3), new Polinome()).Returns(new Polinome(1, 2, 3));
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForAdditing))]
        public Polinome TestAdditing(Polinome Polinome1, Polinome Polinome2)
        {
            return Polinome1 + Polinome2;

        }

        public static IEnumerable<TestCaseData> TestCasesForAdditingThrows
        {
            get
            {
                yield return new TestCaseData(new Polinome(1, 2, 3), null);
                yield return new TestCaseData(null, new Polinome(1, 2, 3));
                yield return new TestCaseData(null, null);
            }
        }

        [Test, TestCaseSource(nameof(TestCasesForAdditingThrows))]
        public void TestAdditingThrows(Polinome Polinome1, Polinome Polinome2)
        {
            Assert.That(() => Polinome1 + Polinome2, Throws.TypeOf<ArgumentNullException>());

        }
        
        #endregion
    }
}
