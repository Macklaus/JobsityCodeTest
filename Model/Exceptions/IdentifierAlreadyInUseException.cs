using System;

namespace Model.Exceptions
{
    [Serializable]
    public class IdentifierAlreadyInUseException : Exception
    {
        public IdentifierAlreadyInUseException() { }

        public IdentifierAlreadyInUseException(string identifier) 
            : base(String.Format("{0} is already in use", identifier)) { }
    }
}
