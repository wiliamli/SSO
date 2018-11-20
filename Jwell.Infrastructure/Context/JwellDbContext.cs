using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using Jwell.Domain.Entities;

namespace Jwell.Repository.Context
{
    public class JwellDbContext: DbContext
    {
        public JwellDbContext()
            : base("Default")
        {

        }

        public JwellDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected JwellDbContext(DbCompiledModel model) : base(model)
        {
        }

        public IDbSet<OAuthService> OAuthService { get; set; }

        public IDbSet<OAuthValidate> OAuthValidate { get; set; }

        #region 配置信息
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 4));
            modelBuilder.HasDefaultSchema("JWELL_SSO");
            base.OnModelCreating(modelBuilder);
            Dasebase databaseType = GetDatabaseType();
            SetDefaultSchema(databaseType, modelBuilder);

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //var sb = new StringBuilder();
                //foreach (var error in ex.EntityValidationErrors)
                //{
                //    foreach (var item in error.ValidationErrors)
                //    {
                //        sb.AppendLine(item.PropertyName + ": " + item.ErrorMessage);
                //    }
                //}
                throw ex;
            }
        }


        private void InitializeContext()
        {
            base.Configuration.UseDatabaseNullSemantics = true;
            base.Configuration.ValidateOnSaveEnabled = false;
        }

        protected virtual void SetDefaultSchema(Dasebase databaseType, DbModelBuilder modelBuilder)
        {
            switch (databaseType)
            {
                case Dasebase.SqlServer:
                    modelBuilder.HasDefaultSchema("dbo");
                    break;
                case Dasebase.Oracle:
                    {
                        string text = base.Database.Connection.ConnectionString.Split(new string[] { ";" },StringSplitOptions.RemoveEmptyEntries)
                            .FirstOrDefault((string p) => p.Trim().StartsWith("User Id", StringComparison.CurrentCultureIgnoreCase));
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            string schema = text.ToUpper().Replace("USER ID", string.Empty).Replace("=", string.Empty)
                                .Trim();
                            modelBuilder.HasDefaultSchema(schema);
                        }
                        break;
                    }
            }
        }

        protected virtual Dasebase GetDatabaseType()
        {
            string name = base.Database.Connection.GetType().Name;
            if (!(name == "SqlConnection"))
            {
                if (name == "OracleConnection")
                {
                    return Dasebase.Oracle;
                }
            }
            return Dasebase.SqlServer;
        }
        #endregion
    }
}
