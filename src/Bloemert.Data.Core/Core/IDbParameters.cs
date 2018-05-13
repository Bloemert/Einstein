using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloemert.Data.Core
{
    public interface IDbParameters
    {
        DynamicParameters DynamicParameters { get; }

        void CreateParamsWithTemplate(object TEMPLATE);

        void Add(string name);
        void Add(string name, object value);
        void Add(string name, object value, DbType? dbType);
        void Add(string name, object value, DbType? dbType, ParameterDirection? direction);
        void Add(string name, object value, DbType? dbType, ParameterDirection? direction, int? size);

        void AddDynamicParams(dynamic param);

        T Get<T>(string name);

        IEnumerable<string> ParameterNames { get; }


        void AddInputParameter(string name, object VALUE);
        void AddInputParameter(string name, object VALUE, DbType? dbType, int? size);

        void AddInputParameter<T>(string name, T VALUE);
        void AddInputParameter<T>(string name, T VALUE, DbType? dbType, int? size);

        void AddOutputParameter(string name);
        void AddOutputParameter(string name, DbType? dbType, int? size);

        void AddReturnValue(string name);
        void AddReturnValue(string name, DbType? dbType, int? size);
    }
}
