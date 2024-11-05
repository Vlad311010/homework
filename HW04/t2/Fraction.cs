using System;
using System.Collections.Generic;

namespace t2
{
    sealed internal class Fraction : IComparable<Fraction>
    {
        private readonly int _numerator;
        private readonly int _denominator;

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator can't be equal to zero");
            }
            _numerator = numerator;
            _denominator = denominator;

            // Simplify;
            if (_numerator < 0) // for convenience fraction sign keeped in denominator
            {
                _numerator = Math.Abs(_numerator);
                _denominator *= -1;
            }
            int gcd = GCD(_numerator, _denominator);
            _numerator /= gcd;
            _denominator /= gcd;
        }

        private int GCD(int num1, int num2) // Euclidean algorythm
        {
            if (num1 == 0 || num2 == 0)
                throw new ArgumentException("Can't calculate common divisor for zero");

            int Remainder;

            while (num2 != 0)
            {
                Remainder = num1 % num2;
                num1 = num2;
                num2 = Remainder;
            }

            return Math.Abs(num1);
        }

        public override string ToString() 
        { 
            return $"{_numerator}\\{_denominator}";
        }

        public override bool Equals(object? obj)
        {
            Fraction other = obj as Fraction;
            if (other == null)
                return false;

            return _denominator == other._denominator && _numerator == other._numerator;
            
        }

        public int CompareTo(Fraction? other)
        {
            if (other == null)
                throw new ArgumentException("Can't compare fraction with null value");

            if (_denominator == other._denominator)
                return _numerator.CompareTo(other._numerator);
            else // cross multiplication
            {
                int num = _numerator;
                int den = _denominator;
                int otherNum = other._numerator;
                int otherDen = other._denominator;

                int res = num * otherDen - den * otherNum;

                return Math.Sign(res);
            }
        }

        public static Fraction operator +(Fraction a) => a;
        public static Fraction operator -(Fraction a) => new Fraction(a._numerator, -a._denominator);

        public static Fraction operator +(Fraction a, Fraction b)
            => new Fraction(a._numerator * b._denominator + b._numerator * a._denominator, a._denominator * b._denominator);

        public static Fraction operator -(Fraction a, Fraction b)
            => a + (-b);

        public static Fraction operator *(Fraction a, Fraction b)
            => new Fraction(a._numerator * b._numerator, a._denominator * b._denominator);

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b._numerator == 0)
            {
                throw new DivideByZeroException();
            }
            return new Fraction(a._numerator * b._denominator, a._denominator * b._numerator);
        }

        public static explicit operator double(Fraction f) => (double)f._numerator / (double)f._denominator;
        public static implicit operator Fraction(int n) => new Fraction(n, 1);
    }
}
