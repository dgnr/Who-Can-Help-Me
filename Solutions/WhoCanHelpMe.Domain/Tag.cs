namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Diagnostics;

    using SharpArch.Core.DomainModel;

    #endregion

    [DebuggerDisplay("{Name}")]
    public class Tag : Entity
    {
        [DomainSignature]
        public virtual string Name { get; set; }

        public virtual int Views { get; set; }
    }
}
