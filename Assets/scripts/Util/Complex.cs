using System;
using UnityEngine;

namespace Util
{
    /// <summary>
    /// Complex number struct with float precision
    /// </summary>
    public readonly struct Complex: IEquatable<Complex>, IComparable<Complex>, IFormattable
    {
        public readonly float Real;
        public readonly float Imaginary;
        
        public Complex(float real, float imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public Complex(Vector3 rot, bool isDeg = false)
        {
            var y = rot.y;
            if (isDeg)
            {
                y *= MathF.PI / 180.0f;
            }
            Real = MathF.Cos(y);
            Imaginary = MathF.Sin(y);
        }

        private unsafe float Invsqrt()
        {
            var x = Real * Real +Imaginary * Imaginary;

            var buf = *(long*)&x;
            buf = 0x5F3759DF - (buf >> 1);
            var y = *(float*)&buf;

            y *= Math.Abs(1.5f - x * 0.5f * y * y);
            return y;
        }
        
        public float Im => Imaginary;
        public float Re => Real;

        public float Magnitude => 1 / Invsqrt();
        public float SqrMagnitude => Real * Real + Imaginary * Imaginary;
        public float Phase => MathF.Atan2(Imaginary, Real);
        public Complex Conjugate => new(Real, -Imaginary);
        
        public Complex Normalize => this * Invsqrt();
        public Vector3 ToVector3 => new(Real, 0.0f, Imaginary);
        
        public static Complex operator +(in Complex a, in Complex b)
        {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }
        
        public static Complex operator -(in Complex a, in Complex b)
        {
            return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }
        
        public static Complex operator *(in Complex a, in Complex b)
        {
            return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        }
        
        public static Complex operator *(in Complex a, in float b)
        {
            return new Complex(a.Real * b, a.Imaginary * b);
        }
        
        public static Complex operator *(in float a, in Complex b)
        {
            return new Complex(a * b.Real, a * b.Imaginary);
        }
        
        public static Complex operator /(in Complex a, in float b)
        {
            return new Complex(a.Real / b, a.Imaginary / b);
        }
        
        public static Complex operator /(in float a, in Complex b)
        {
            return new Complex(a * b.Real, a * b.Imaginary);
        }
        
        public static Complex operator /(in Complex a, in Complex b)
        {
            return new Complex(a.Real * b.Real + a.Imaginary * b.Imaginary, a.Imaginary * b.Real - a.Real * b.Imaginary) / (b.Real * b.Real + b.Imaginary * b.Imaginary);
        }
        
        public static Complex operator -(in Complex a)
        {
            return new Complex(-a.Real, -a.Imaginary);
        }
        
        public static Complex operator ~(in Complex a)
        {
            return new Complex(a.Real, -a.Imaginary);
        }
        
        public static Complex operator ++(in Complex a)
        {
            return new Complex(a.Real + 1, a.Imaginary);
        }
        
        public static Complex operator --(in Complex a)
        {
            return new Complex(a.Real - 1, a.Imaginary);
        }

        public bool Equals(Complex other)
        {
            return Real.Equals(other.Real) && Imaginary.Equals(other.Imaginary);
        }

        public override bool Equals(object obj)
        {
            return obj is Complex other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }
        
        public override string ToString(){
            return $"{Real}+{Imaginary}i";
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public int CompareTo(Complex other)
        {
            var realComparison = Real.CompareTo(other.Real);
            return realComparison != 0 ? realComparison : Imaginary.CompareTo(other.Imaginary);
        }
    }
}