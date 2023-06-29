using Util;

namespace IO
{
    /// <summary>
    /// Interface for providing move direction
    /// </summary>
    public interface IMoveProvider
    {
        public Complex GetMove();
    }
}
