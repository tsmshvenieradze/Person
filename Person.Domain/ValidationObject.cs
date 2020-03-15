namespace Person.Domain
{
   public abstract class ValidationObject
    {

        public virtual bool Validate()
        {
           return true;
        }
    }
}
