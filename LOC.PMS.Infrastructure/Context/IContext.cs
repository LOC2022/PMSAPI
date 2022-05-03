using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

public interface IContext
{


    /// <summary>
    ///     ''' Database Connection Timeout
    ///     ''' </summary>
    int? ConnectionTimeout { get; set; }

    /// <summary>
    ///     ''' Execute the SQL as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <param name="sql">the sql to execute</param>
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=sql is empty. 
    ///     ''' Throw Exception if output parameters are pass.
    ///     ''' </remarks>
    void ExecuteSql(string sql, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the SQL as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <param name="sql">the sql to execute</param>
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=sql is empty. 
    ///     ''' Throw Exception if output parameters are pass.
    ///     ''' </remarks>
    void ExecuteSql(string sql, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the SQL as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <param name="sql">the sql to execute</param>
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>cref="Task"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=sql is empty. 
    ///     ''' Throw Exception if output parameters are pass.
    ///     ''' </remarks>
    Task ExecuteSqlAsync(string sql, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the SQL as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <param name="sql">the sql to execute</param>
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>cref="Task"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=sql is empty. 
    ///     ''' Throw Exception if output parameters are pass.
    ///     ''' </remarks>
    Task ExecuteSqlAsync(string sql, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the SQL as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>T</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name="sql" is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    T ExecuteSql<T>(string sql, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the SQL as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>T</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=sql is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    T ExecuteSql<T>(string sql, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the SQL as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>cref="Task(Of T)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=sql is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<T> ExecuteSqlAsync<T>(string sql, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the SQL as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns> cref="Task(Of T)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=sql is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<T> ExecuteSqlAsync<T>(string sql, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute all the request in scope transaction
    ///     ''' </summary>
    ///     ''' <param name="transactionScopeRequests">list of command to execute</param>
    ///     ''' <remarks></remarks>
    void ExecuteStoredProcedureWithTransactionScope(List<ICommandsWithParams> transactionScopeRequests);

    /// <summary>
    ///     ''' Execute all the request in scope transaction
    ///     ''' </summary>
    ///     ''' <typeparam name="T1"></typeparam>
    ///     ''' <param name="transactionScopeRequests"></param>
    ///     ''' <returns>IEnumerable(Of T1)</returns>
    IEnumerable<T1> QueryStoredProcedureWithTransactionScope<T1>(List<ICommandsWithParams> transactionScopeRequests);


    /// <summary>
    ///     ''' Query Store Procedure with in transaction using the Isolation level 
    ///     ''' </summary>
    ///     ''' <param name="storedProcedureName">the store procedure name to execute</param> 
    ///     ''' <param name="transactionLevel">scope of the transaction</param>
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <remarks></remarks>
    IEnumerable<T> QueryStoreProcedureWithIsolationLevel<T>(string storedProcedureName, System.Data.IsolationLevel transactionLevel, params IDbDataParameter[] parameters);


    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if output parameters are pass.
    ///     ''' </remarks>
    void ExecuteStoredProcedure(string storeProcedureName, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if output parameters are pass.
    ///     ''' </remarks>
    void ExecuteStoredProcedure(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>cref="Task"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=sql is empty. 
    ///     ''' Throw Exception if output parameters are pass.
    ///     ''' </remarks>
    Task ExecuteStoredProcedureAsync(string storeProcedureName, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>  
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>cref="Task"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if output parameters are pass.
    ///     ''' </remarks>
    Task ExecuteStoredProcedureAsync(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>T</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    T ExecuteStoredProcedure<T>(string storeProcedureName, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <returns>T</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    T ExecuteStoredProcedure<T>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>cref="Task(Of T)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<T> ExecuteStoredProcedureAsync<T>(string storeProcedureName, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>cref="Task(Of T)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<T> ExecuteStoredProcedureAsync<T>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);



    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type1</typeparam>
    ///     ''' <typeparam name="T2">the output type2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <returns>cref="Tuple(Of T1, T2)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Tuple<T1, T2> ExecuteStoredProcedure<T1, T2>(string storeProcedureName, params IDbDataParameter[] parameters);


    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <returns>cref="Tuple(Of T1, T2)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Tuple<T1, T2> ExecuteStoredProcedure<T1, T2>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>cref="Task(Of Tuple(Of T1, T2))"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<Tuple<T1, T2>> ExecuteStoredProcedureAsync<T1, T2>(string storeProcedureName, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>cref="Task(Of Tuple(Of T1, T2))"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<Tuple<T1, T2>> ExecuteStoredProcedureAsync<T1, T2>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);


    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type1</typeparam>
    ///     ''' <typeparam name="T2">the output type2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <returns>cref="Tuple(Of T1, T2)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Tuple<T1, T2, T3> ExecuteStoredProcedure<T1, T2, T3>(string storeProcedureName, params IDbDataParameter[] parameters);


    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <returns>cref="Tuple(Of T1, T2)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Tuple<T1, T2, T3> ExecuteStoredProcedure<T1, T2, T3>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);


    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>cref="Task(Of Tuple(Of T1, T2))"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<Tuple<T1, T2, T3>> ExecuteStoredProcedureAsync<T1, T2, T3>(string storeProcedureName, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>cref="Task(Of Tuple(Of T1, T2))"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<Tuple<T1, T2, T3>> ExecuteStoredProcedureAsync<T1, T2, T3>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type1</typeparam>
    ///     ''' <typeparam name="T2">the output type2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <returns>cref="Tuple(Of T1, T2)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Tuple<T1, T2, T3, T4> ExecuteStoredProcedure<T1, T2, T3, T4>(string storeProcedureName, params IDbDataParameter[] parameters);


    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <returns>cref="Tuple(Of T1, T2)"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Tuple<T1, T2, T3, T4> ExecuteStoredProcedure<T1, T2, T3, T4>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);


    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>cref="Task(Of Tuple(Of T1, T2))"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<Tuple<T1, T2, T3, T4>> ExecuteStoredProcedureAsync<T1, T2, T3, T4>(string storeProcedureName, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteNonQuery()
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="useTransaction">determine if the execute should be in a transaction</param>  
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>cref="Task(Of Tuple(Of T1, T2))"</returns>
    ///     ''' <remarks>
    ///     ''' Throw ArgumentNullException if name=storeProcedureName is empty. 
    ///     ''' Throw Exception if no output parameters are pass.
    ///     ''' </remarks>
    Task<Tuple<T1, T2, T3, T4>> ExecuteStoredProcedureAsync<T1, T2, T3, T4>(string storeProcedureName, bool useTransaction, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the sql as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>   
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>IEnumerable(Of T)</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=sql is empty.</remarks>
    IEnumerable<T> QueryData<T>(string sql, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the sql as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>   
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>IEnumerable(Of T)</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=sql is empty.</remarks>
    Task<IEnumerable<T>> QueryDataAsync<T>(string sql, params IDbDataParameter[] parameters);

    /// <summary>
    ///     ''' Execute the sql as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>   
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=sql is empty.</remarks>
    Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryDataMultiple<T1, T2>(string sql, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new();

    /// <summary>
    ///     ''' Execute the sql as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <typeparam name="T3">the output type 3</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>   
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2), IEnumerable(Of T3))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=sql is empty.</remarks>
    Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryDataMultiple<T1, T2, T3>(string sql, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new();

    /// <summary>
    ///     ''' Execute the sql as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>    
    ///     ''' <param name="sql">the sql to execute</param>   
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>Task(Of Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2)))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=sql is empty.</remarks>
    Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> QueryDataMultipleAsync<T1, T2>(string sql, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new();

    /// <summary>
    ///     ''' Execute the sql as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <typeparam name="T3">the output type 3</typeparam>
    ///     ''' <param name="sql">the sql to execute</param>   
    ///     ''' <param name="parameters">paramameters use in the SQL</param>
    ///     ''' <returns>Task(Of Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2), IEnumerable(Of T3)))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=sql is empty.</remarks>
    Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> QueryDataMultipleAsync<T1, T2, T3>(string sql, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new();

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>IEnumerable(Of T)</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    IEnumerable<T> QueryStoredProcedure<T>(string storeProcedureName, params IDbDataParameter[] parameters) where T : class, new();

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T">the output type</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>Task(Of IEnumerable(Of T))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    Task<IEnumerable<T>> QueryStoredProcedureAsync<T>(string storeProcedureName, params IDbDataParameter[] parameters) where T : class, new();

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryStoredProcedureMultiple<T1, T2>(string storeProcedureName, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new();

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>Task(Of Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2)))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> QueryStoredProcedureMultipleAsync<T1, T2>(string storeProcedureName, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new();

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <typeparam name="T3">the output type 3</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2), IEnumerable(Of T3))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryStoredProcedureMultiple<T1, T2, T3>(string storeProcedureName, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new();

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <typeparam name="T3">the output type 3</typeparam>
    ///     ''' <typeparam name="T4">the output type 3</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2), IEnumerable(Of T3), IEnumerable(Of T4))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryStoredProcedureMultiple<T1, T2, T3, T4>(string storeProcedureName, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new();

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <typeparam name="T3">the output type 3</typeparam>    
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>Task(Of Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2), IEnumerable(Of T3)))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>> QueryStoredProcedureMultipleAsync<T1, T2, T3>(string storeProcedureName, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new();

    /// <summary>
    ///     ''' Execute the store procedure as ExecuteReader() 
    ///     ''' </summary>
    ///     ''' <typeparam name="T1">the output type 1</typeparam>
    ///     ''' <typeparam name="T2">the output type 2</typeparam>
    ///     ''' <typeparam name="T3">the output type 3</typeparam>
    ///     ''' <typeparam name="T4">the output type 4</typeparam>
    ///     ''' <param name="storeProcedureName">the store procedure name to execute</param>    
    ///     ''' <param name="parameters">paramameters use in the store procedure</param>
    ///     ''' <returns>Task(Of Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2), IEnumerable(Of T3), IEnumerable(Of T4)))</returns>
    ///     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    Task<Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>> QueryStoredProcedureMultipleAsync<T1, T2, T3, T4>(string storeProcedureName, params IDbDataParameter[] parameters)
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where T4 : class, new();
    
    
    void BulkCopy<T>(IEnumerable<T> list, int batchSize, string tableName);

    ///// <summary>
    /////     ''' Execute the store procedure as BulkInsert() 
    /////     ''' </summary>
    /////     ''' <param name="list">list of Entity or model</param>    
    /////     ''' <param name="batchSize">batch size for insert</param>
    /////     ''' <param name="insertIfNotExists">insert record if not exists</param>
    /////     ''' <returns>Task(Of Tuple(Of IEnumerable(Of T1), IEnumerable(Of T2), IEnumerable(Of T3), IEnumerable(Of T4)))</returns>
    /////     ''' <remarks> throw ArgumentNullException if name=storeProcedureName is empty.</remarks>
    //void BulkCopy<T>(T list, int batchSize, bool insertIfNotExists = true);


}

public interface ICommandsWithParams
{
    string CommandText { get; set; }
    CommandType CommandType { get; set; }
    List<IDbDataParameter> CommandParams { get; set; }
}
