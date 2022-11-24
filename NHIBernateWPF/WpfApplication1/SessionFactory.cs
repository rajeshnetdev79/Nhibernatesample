using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public sealed class SessionFactory
    {
        private static volatile ISessionFactory iSessionFactory;

        private static object syncRoot = new Object();

        public static ISession OpenSession

        {
            get
            {
                if (iSessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            Configuration configuration = new Configuration();
                            configuration.Configure();
                            iSessionFactory = configuration.BuildSessionFactory();
                        }
                    }
                }
                return iSessionFactory.OpenSession();

            }

        }
    }
}
