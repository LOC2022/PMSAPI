using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Microsoft.Extensions.Configuration;
//using Z.Dapper.Plus;
//using Z.BulkOperations;

namespace LOC.PMS.Infrastructure
{
    public class Context : IContext
    {
        private readonly string connectionString;
        public Context(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int? ConnectionTimeout { get; set; }

        #region Execute

        public void ExecuteSql(string sql, params IDbDataParameter[] parameters)
        {
            this.ExecuteSql(sql, false, parameters);
        }

        public void ExecuteSql(string sql, bool useTransaction, params IDbDataParameter[] parameters)
        {

            var task = Task.Run(() => this.ExecuteSqlAsync(sql, useTransaction, parameters));
            task.Wait();
        }

        public async Task ExecuteSqlAsync(string sql, params IDbDataParameter[] parameters)
        {
            await this.ExecuteSqlAsync(sql, false, parameters);
        }

        public async Task ExecuteSqlAsync(string sql, bool useTransaction, params IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            var convertResult = translateToConvertResult(parameters);

            if (convertResult.ReturnParameterNames.Count > 0)
                throw new Exception("return parameter is specify, please use ExecuteSQL<T> or  ExecuteSQLAsync<T>");

            IDbTransaction transaction = null;

            using (var conn = new SqlConnection(this.connectionString))
            {
                if (useTransaction)
                    transaction = conn.BeginTransaction();

                try
                {
                    await conn.ExecuteAsync(sql, convertResult.DynamicParameters, commandTimeout: this.ConnectionTimeout);
                    if (useTransaction)
                        transaction.Commit();
                }
                catch (Exception)
                {
                    if (useTransaction)
                        transaction.Rollback();
                    throw;
                }
            }

        }

        public T ExecuteSql<T>(string sql, params IDbDataParameter[] parameters)
        {
            return this.ExecuteSql<T>(sql, false, parameters);
        }

        public T ExecuteSql<T>(string sql, bool useTransaction, params IDbDataParameter[] parameters)
        {
            var task = Task.Run(() => this.ExecuteSqlAsync<T>(sql, useTransaction, parameters));
            task.Wait();
            return task.Result;
        }

        public async Task<T> ExecuteSqlAsync<T>(string sql, params IDbDataParameter[] parameters)
        {
            return await this.ExecuteSqlAsync<T>(sql, false, parameters);
        }

        public async Task<T> ExecuteSqlAsync<T>(string sql, bool useTransaction, params IDbDataParameter[] parameters)
        {

            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            var convertResult = translateToConvertResult(parameters);

            if (convertResult.ReturnParameterNames.Count == 0)
                throw new Exception("no return parameter is specify");

            if (convertResult.ReturnParameterNames.Count > 1)
                throw new Exception("multiples return parameters are specify");

            IDbTransaction transaction = null;

            using (var conn = new SqlConnection(this.connectionString))
            {

                if (useTransaction)
                    transaction = conn.BeginTransaction();

                try
                {

                    await conn.ExecuteAsync(sql, convertResult.DynamicParameters, commandTimeout: this.ConnectionTimeout);
                    if (useTransaction)
                        transaction.Commit();

                }
                catch (Exception)
                {
                    if (useTransaction)
                        transaction.Rollback();
                    throw;
                }
            }

            return convertResult.DynamicParameters.Get<T>(convertResult.ReturnParameterNames[0]);
        }

        public void ExecuteStoredProcedure(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            this.ExecuteStoredProcedure(storeProcedureName, false, parameters);
        }

        public void ExecuteStoredProcedure(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            var task = Task.Run(() => this.ExecuteStoredProcedureAsync(storeProcedureName, useTransaction, parameters));
            task.Wait();
        }

        public async Task ExecuteStoredProcedureAsync(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            await this.ExecuteStoredProcedureAsync(storeProcedureName, false, parameters);
        }

        public async Task ExecuteStoredProcedureAsync(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            if (convertResult.ReturnParameterNames.Count > 0)
                throw new Exception("return parameter is specify, please use ExecuteStoreProcedureAsync<T> or ExecuteStoreProcedureAsync<T>");

            IDbTransaction transaction = null;

            using (var conn = new SqlConnection(this.connectionString))
            {

                if (useTransaction)
                    transaction = conn.BeginTransaction();
                try
                {
                    await conn.ExecuteAsync(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);
                    if (useTransaction)
                        transaction.Commit();
                }
                catch (Exception)
                {
                    if (useTransaction)
                        transaction.Rollback();
                    throw;
                }
            }
        }

        public T ExecuteStoredProcedure<T>(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            return this.ExecuteStoredProcedure<T>(storeProcedureName, false, parameters);
        }

        public T ExecuteStoredProcedure<T>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            var task = Task.Run(() => this.ExecuteStoredProcedureAsync<T>(storeProcedureName, useTransaction, parameters));
            task.Wait();
            return task.Result;
        }

        public async Task<T> ExecuteStoredProcedureAsync<T>(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            return await this.ExecuteStoredProcedureAsync<T>(storeProcedureName, false, parameters);
        }

        public async Task<T> ExecuteStoredProcedureAsync<T>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {

            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = translateToConvertResult(parameters);

            if (convertResult.ReturnParameterNames.Count == 0)
                throw new Exception("no return parameter is specify");

            if (convertResult.ReturnParameterNames.Count > 1)
                throw new Exception("multiples return parameters are specify");

            T returnValue = default(T);

            using (var conn = new SqlConnection(this.connectionString))
            {
                IDbTransaction transaction = null;
                if (useTransaction)
                    transaction = conn.BeginTransaction();

                try
                {
                    await conn.ExecuteAsync(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);

                    if (useTransaction)
                        transaction.Commit();
                }
                catch (Exception)
                {
                    if (useTransaction)
                        transaction.Rollback();
                    throw;
                }

                returnValue = convertResult.DynamicParameters.Get<T>(convertResult.ReturnParameterNames[0]);

            }

            return returnValue;
        }

        public Tuple<T1, T2> ExecuteStoredProcedure<T1, T2>(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            return this.ExecuteStoredProcedure<T1, T2>(storeProcedureName, false, parameters);
        }

        public Tuple<T1, T2> ExecuteStoredProcedure<T1, T2>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            var task = Task.Run(() => this.ExecuteStoredProcedureAsync<T1, T2>(storeProcedureName, useTransaction, parameters));
            task.Wait();
            return task.Result;
        }

        public async Task<Tuple<T1, T2>> ExecuteStoredProcedureAsync<T1, T2>(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            return await this.ExecuteStoredProcedureAsync<T1, T2>(storeProcedureName, false, parameters);
        }


        public async Task<Tuple<T1, T2>> ExecuteStoredProcedureAsync<T1, T2>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            if (convertResult.ReturnParameterNames.Count == 0)
                throw new Exception("return parameter is not specify, please use ExecuteStoreProcedureAsync or  ExecuteStoreProcedureAsync");

            if (convertResult.ReturnParameterNames.Count != 2)
                throw new Exception("invalid number of return parameters");

            IDbTransaction transaction = null;

            T1 returnParmValue1 = default(T1);
            T2 returnParmValue2 = default(T2);

            using (var conn = new SqlConnection(this.connectionString))
            {

                if (useTransaction)
                    transaction = conn.BeginTransaction();
                try
                {
                    await conn.ExecuteAsync(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);
                    if (useTransaction)
                        transaction.Commit();
                }
                catch (Exception)
                {
                    if (useTransaction)
                        transaction.Rollback();
                    throw;
                }

                returnParmValue1 = convertResult.DynamicParameters.Get<T1>(convertResult.ReturnParameterNames[0]);
                returnParmValue2 = convertResult.DynamicParameters.Get<T2>(convertResult.ReturnParameterNames[1]);
            }


            return Tuple.Create<T1, T2>(returnParmValue1, returnParmValue2);
        }

        public Tuple<T1, T2, T3> ExecuteStoredProcedure<T1, T2, T3>(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            return this.ExecuteStoredProcedure<T1, T2, T3>(storeProcedureName, false, parameters);
        }

        public Tuple<T1, T2, T3> ExecuteStoredProcedure<T1, T2, T3>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            var task = Task.Run(() => this.ExecuteStoredProcedure<T1, T2, T3>(storeProcedureName, useTransaction, parameters));
            task.Wait();
            return task.Result;
        }

        public async Task<Tuple<T1, T2, T3>> ExecuteStoredProcedureAsync<T1, T2, T3>(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            return await this.ExecuteStoredProcedureAsync<T1, T2, T3>(storeProcedureName, false, parameters);
        }

        public async Task<Tuple<T1, T2, T3>> ExecuteStoredProcedureAsync<T1, T2, T3>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            if (convertResult.ReturnParameterNames.Count == 0)
                throw new Exception("return parameter is specify, please use ExecuteStoreProcedureAsync or  ExecuteStoreProcedureAsync");

            if (convertResult.ReturnParameterNames.Count != 3)
                throw new Exception("invalid number of return parameters");

            IDbTransaction transaction = null;

            T1 returnParmValue1 = default(T1);
            T2 returnParmValue2 = default(T2);
            T3 returnParmValue3 = default(T3);

            using (var conn = new SqlConnection(this.connectionString))
            {

                if (useTransaction)
                    transaction = conn.BeginTransaction();
                try
                {
                    await conn.ExecuteAsync(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);
                    if (useTransaction)
                        transaction.Commit();
                }
                catch (Exception)
                {
                    if (useTransaction)
                        transaction.Rollback();
                    throw;
                }

                returnParmValue1 = convertResult.DynamicParameters.Get<T1>(convertResult.ReturnParameterNames[0]);
                returnParmValue2 = convertResult.DynamicParameters.Get<T2>(convertResult.ReturnParameterNames[1]);
                returnParmValue3 = convertResult.DynamicParameters.Get<T3>(convertResult.ReturnParameterNames[2]);
            }


            return Tuple.Create<T1, T2, T3>(returnParmValue1, returnParmValue2, returnParmValue3);

        }

        public Tuple<T1, T2, T3, T4> ExecuteStoredProcedure<T1, T2, T3, T4>(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            return this.ExecuteStoredProcedure<T1, T2, T3, T4>(storeProcedureName, false, parameters);
        }

        public Tuple<T1, T2, T3, T4> ExecuteStoredProcedure<T1, T2, T3, T4>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            var task = Task.Run(() => this.ExecuteStoredProcedureAsync<T1, T2, T3, T4>(storeProcedureName, useTransaction, parameters));
            task.Wait();
            return task.Result;

        }

        public async Task<Tuple<T1, T2, T3, T4>> ExecuteStoredProcedureAsync<T1, T2, T3, T4>(string storeProcedureName, params IDbDataParameter[] parameters)
        {
            return await this.ExecuteStoredProcedureAsync<T1, T2, T3, T4>(storeProcedureName, false, parameters);
        }


        public async Task<Tuple<T1, T2, T3, T4>> ExecuteStoredProcedureAsync<T1, T2, T3, T4>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            if (convertResult.ReturnParameterNames.Count == 0)
                throw new Exception("return parameter is specify, please use ExecuteStoreProcedureAsync or  ExecuteStoreProcedureAsync");

            if (convertResult.ReturnParameterNames.Count != 4)
                throw new Exception("invalid number of return parameters");

            IDbTransaction transaction = null;

            T1 returnParmValue1 = default(T1);
            T2 returnParmValue2 = default(T2);
            T3 returnParmValue3 = default(T3);
            T4 returnParmValue4 = default(T4);

            using (var conn = new SqlConnection(this.connectionString))
            {

                if (useTransaction)
                    transaction = conn.BeginTransaction();
                try
                {
                    await conn.ExecuteAsync(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);
                    if (useTransaction)
                        transaction.Commit();
                }
                catch (Exception)
                {
                    if (useTransaction)
                        transaction.Rollback();
                    throw;
                }

                returnParmValue1 = convertResult.DynamicParameters.Get<T1>(convertResult.ReturnParameterNames[0]);
                returnParmValue2 = convertResult.DynamicParameters.Get<T2>(convertResult.ReturnParameterNames[1]);
                returnParmValue3 = convertResult.DynamicParameters.Get<T3>(convertResult.ReturnParameterNames[2]);
                returnParmValue4 = convertResult.DynamicParameters.Get<T4>(convertResult.ReturnParameterNames[3]);

            }


            return Tuple.Create<T1, T2, T3, T4>(returnParmValue1, returnParmValue2, returnParmValue3, returnParmValue4);
        }


        #endregion

        #region TransactionScope

        public void ExecuteStoredProcedureWithTransactionScope(List<ICommandsWithParams> transactionScopeRequests)
        {

            try
            {
                using (var scopeTransaction = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(60) }))
                {
                    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    {
                        conn.Open();
                        foreach (ICommandsWithParams request in transactionScopeRequests)
                        {
                            using (SqlCommand cmd = new SqlCommand(request.CommandText, conn))
                            {
                                cmd.CommandType = request.CommandType;
                                cmd.Parameters.AddRange(request.CommandParams.ToArray());
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    scopeTransaction.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T1> QueryStoredProcedureWithTransactionScope<T1>(List<ICommandsWithParams> transactionScopeRequests)
        {
            List<T1> result = new List<T1>();
            try
            {
                using (var scopeTransaction = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted, Timeout = TimeSpan.FromSeconds(60) }))
                {
                    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    {
                        conn.Open();
                        foreach (ICommandsWithParams request in transactionScopeRequests)
                        {
                            var convertResult = translateToConvertResult(request.CommandParams.ToArray());
                            CommandDefinition dcmd = new CommandDefinition(request.CommandText, convertResult.DynamicParameters, null, null, request.CommandType);
                            IEnumerable<T1> result2 = conn.Query<T1>(dcmd);
                            T1 outputVal = convertResult.DynamicParameters.Get<T1>(convertResult.ReturnParameterNames[0]);
                            result.Add(outputVal);
                        }
                    }
                    scopeTransaction.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            return result;
        }

        public IEnumerable<T> QueryStoreProcedureWithIsolationLevel<T>(string storedProcedureName, System.Data.IsolationLevel transactionIsolationLevel, params IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(storedProcedureName))
                throw new ArgumentNullException("storedProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            IEnumerable<T> result;

            var convertedTransactionLevel = (System.Transactions.IsolationLevel)Enum.Parse(typeof(System.Transactions.IsolationLevel), transactionIsolationLevel.ToString());

            using (var scopetransaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = convertedTransactionLevel }))
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        result = conn.Query<T>(storedProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);
                    }
                }
                scopetransaction.Complete();
            }

            return result;

        }

        #endregion

        #region Query

        public IEnumerable<T> QueryData<T>(string sql, params IDbDataParameter[] parameters)
        {
            var task = Task.Run(() => this.QueryDataAsync<T>(sql, parameters));
            task.Wait();
            return task.Result;
        }

        public async Task<IEnumerable<T>> QueryDataAsync<T>(string sql, params IDbDataParameter[] parameters)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            var convertResult = translateToConvertResult(parameters);

            IEnumerable<T> result;
            using (var conn = new SqlConnection(this.connectionString))
            {
                result = await conn.QueryAsync<T>(sql, convertResult.DynamicParameters, commandTimeout: this.ConnectionTimeout);
            }

            return result;
        }

        public IEnumerable<T> QueryData<T>(string sql, object criteria)
        {
            var task = Task.Run(() => this.QueryDataAsync<T>(sql, criteria));
            task.Wait();
            return task.Result;
        }

        public async Task<IEnumerable<T>> QueryDataAsync<T>(string sql, object criteria)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            IEnumerable<T> result;
            using (var conn = new SqlConnection(this.connectionString))
            {
                result = await conn.QueryAsync<T>(sql, criteria, commandTimeout: this.ConnectionTimeout);
            }

            return result;
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryDataMultiple<T1, T2>(string sql, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
        {
            var task = Task.Run(() => this.QueryDataMultipleAsync<T1, T2>(sql, parameters));
            task.Wait();
            return task.Result;

        }

        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> QueryDataMultipleAsync<T1, T2>(string sql, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            var convertResult = translateToConvertResult(parameters);

            IEnumerable<T1> returnType1;
            IEnumerable<T2> returnType2;
            using (var conn = new SqlConnection(this.connectionString))
            {
                var result = await conn.QueryMultipleAsync(sql, convertResult.DynamicParameters, commandTimeout: this.ConnectionTimeout);
                returnType1 = await result.ReadAsync<T1>();
                returnType2 = await result.ReadAsync<T2>();
            }

            return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>>(returnType1, returnType2);

        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryDataMultiple<T1, T2, T3>(string sql, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            var task = Task.Run(() => this.QueryDataMultipleAsync<T1, T2, T3>(sql, parameters));
            task.Wait();
            return task.Result;

        }

        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> QueryDataMultipleAsync<T1, T2, T3>(string sql, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            var convertResult = translateToConvertResult(parameters);

            IEnumerable<T1> returnType1;
            IEnumerable<T2> returnType2;
            IEnumerable<T3> returnType3;

            using (var conn = new SqlConnection(this.connectionString))
            {
                var result = await conn.QueryMultipleAsync(sql, convertResult.DynamicParameters, commandTimeout: this.ConnectionTimeout);
                returnType1 = await result.ReadAsync<T1>();
                returnType2 = await result.ReadAsync<T2>();
                returnType3 = await result.ReadAsync<T3>();
            }

            return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(returnType1, returnType2, returnType3);

        }

        public IEnumerable<T> QueryStoredProcedure<T>(string storeProcedureName, params IDbDataParameter[] parameters)
            where T : class, new()
        {
            var task = Task.Run(() => this.QueryStoredProcedureAsync<T>(storeProcedureName, parameters));
            task.Wait();
            return task.Result;
        }

        public async Task<IEnumerable<T>> QueryStoredProcedureAsync<T>(string storeProcedureName, params IDbDataParameter[] parameters)
            where T : class, new()
        {
            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            using (var conn = new SqlConnection(this.connectionString))
            {
                return await conn.QueryAsync<T>(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryStoredProcedureMultiple<T1, T2>(string storeProcedureName, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
        {

            var task = Task.Run(() => this.QueryStoredProcedureMultipleAsync<T1, T2>(storeProcedureName, parameters));
            task.Wait();
            return task.Result;

        }

        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> QueryStoredProcedureMultipleAsync<T1, T2>(string storeProcedureName, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
        {
            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            IEnumerable<T1> resultType1;
            IEnumerable<T2> resultType2;

            using (var conn = new SqlConnection(this.connectionString))
            {
                var result = await conn.QueryMultipleAsync(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);

                resultType1 = await result.ReadAsync<T1>();
                resultType2 = await result.ReadAsync<T2>();
            }

            return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>>(resultType1, resultType2);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryStoredProcedureMultiple<T1, T2, T3>(string storeProcedureName, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            var task = Task.Run(() => this.QueryStoredProcedureMultipleAsync<T1, T2, T3>(storeProcedureName, parameters));
            task.Wait();
            return task.Result;
        }

        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> QueryStoredProcedureMultipleAsync<T1, T2, T3>(string storeProcedureName, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {
            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            IEnumerable<T1> returnType1;
            IEnumerable<T2> returnType2;
            IEnumerable<T3> returnType3;

            using (var conn = new SqlConnection(this.connectionString))
            {
                var result = await conn.QueryMultipleAsync(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);
                returnType1 = await result.ReadAsync<T1>();
                returnType2 = await result.ReadAsync<T2>();
                returnType3 = await result.ReadAsync<T3>();
            }


            return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(returnType1, returnType2, returnType3);
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryStoredProcedureMultiple<T1, T2, T3, T4>(string storeProcedureName, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            var task = Task.Run(() => this.QueryStoredProcedureMultipleAsync<T1, T2, T3, T4>(storeProcedureName, parameters));
            task.Wait();
            return task.Result;
        }

        public async Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>> QueryStoredProcedureMultipleAsync<T1, T2, T3, T4>(string storeProcedureName, params IDbDataParameter[] parameters)
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
            where T4 : class, new()
        {
            if (string.IsNullOrEmpty(storeProcedureName))
                throw new ArgumentNullException("storeProcedureName");

            var convertResult = this.translateToConvertResult(parameters);

            IEnumerable<T1> returnType1;
            IEnumerable<T2> returnType2;
            IEnumerable<T3> returnType3;
            IEnumerable<T4> returnType4;

            using (var conn = new SqlConnection(this.connectionString))
            {
                var result = await conn.QueryMultipleAsync(storeProcedureName, convertResult.DynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: this.ConnectionTimeout);
                returnType1 = await result.ReadAsync<T1>();
                returnType2 = await result.ReadAsync<T2>();
                returnType3 = await result.ReadAsync<T3>();
                returnType4 = await result.ReadAsync<T4>();

            }


            return Tuple.Create<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(returnType1, returnType2, returnType3, returnType4);
        }

        #endregion

        #region BulkOperation

        public async void BulkCopy<T>(IEnumerable<T> list, IEnumerable<T> dlist, int batchSize, string tableName)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                DataTable dtInsertRows;

                //connection.bulk (options =>
                //{
                //    options.InsertIfNotExists = insertIfNotExists;
                //    options.BatchSize = batchSize;
                //}).BulkInsert(list);
                await connection.OpenAsync();

                using (var bulkCopy = new SqlBulkCopy(this.connectionString))
                {

                    //for (int i = 0; i <= list.AsList().Count; i++)
                    //{
                    //    if (list[i] = dlist[i])
                    //    {
                    //        bulkCopy.ColumnMappings.Add(col, dlist);

                    //    }
                    //}
                    bulkCopy.BulkCopyTimeout = 120;
                    bulkCopy.BatchSize = batchSize;
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.EnableStreaming = true;

                    using (var dataReader = list.GetDataReader())
                    {
                        await bulkCopy.WriteToServerAsync(dataReader);
                    }
                }

            }
        }

        #endregion

        private ParameterConversionResult translateToConvertResult(params IDbDataParameter[] parameters)
        {
            var convertResults = new ParameterConversionResult();
            if (parameters.Length > 0)
            {
                foreach (var parameter in parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output | parameter.Direction == ParameterDirection.ReturnValue)
                        convertResults.ReturnParameterNames.Add(parameter.ParameterName);

                    convertResults.DynamicParameters.Add(parameter.ParameterName, parameter.Value, parameter.DbType, parameter.Direction, parameter.Size);
                }
            }

            return convertResults;
        }

        private class ParameterConversionResult
        {
            public ParameterConversionResult()
            {
                this.DynamicParameters = new DynamicParameters();
                this.ReturnParameterNames = new List<string>();
            }

            public DynamicParameters DynamicParameters { get; set; }
            public List<string> ReturnParameterNames { get; set; }

        }

    }
}
