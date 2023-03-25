ContatosController

GetId()
http://localhost:5188/Contatos/1

200 OK:
{
"hospital": null,
"idHospital": 1,
"idContato": 1,
"tipoContato": null,
"idTipoContato": 1,
"tipoContatoEnum": 0,
"dsContato": "(11) 3758-5202",
"inContato": null
}

Como faz para trazer todos os contatos do hospital informado no id?
Tentei colocar via Include e ThenInclude na programação do método mas não consegui.

---

UsuariosController

Cadastrar()
http://localhost:5188/Usuarios/Cadastrar
{
"NomeUsuario":"APFL",
"PasswordString":"123321",
"Email": "aparecida_fabiana_lopes@gmail.com",
"CPF": "870.853.739-90"
}

Coloquei o CPF errado para cair na validação do CPF (CPF correto 870.853.739-97), mas o retorno foi 400 BAD REQUEST: Value cannot be null. (Parameter 'value')

Microsoft.EntityFrameworkCore.Database.Command[20101]
Executed DbCommand (2ms) [Parameters=[@__ToLower_0='?' (Size = 4000)], CommandType='Text', CommandTimeout='30']
SELECT CASE
WHEN EXISTS (
SELECT 1
FROM [Usuario] AS [u]
WHERE LOWER([u].[NomeUsuario]) = @\_\_ToLower_0) THEN CAST(1 AS bit)
ELSE CAST(0 AS bit)
END

---

UsuariosController

CadastrarAdmin()
http://localhost:5188/Usuarios/CadastrarAdmin
{
"NomeUsuario":"Admin",
"PasswordString":"123321"
}

400 BAD REQUEST: An error occurred while saving the entity changes. See the inner exception for details.

warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
Failed to determine the https port for redirect.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
Executed DbCommand (36ms) [Parameters=[@__ToLower_0='?' (Size = 4000)], CommandType='Text', CommandTimeout='30']
SELECT CASE
WHEN EXISTS (
SELECT 1
FROM [Usuario] AS [u]
WHERE LOWER([u].[NomeUsuario]) = @**ToLower_0) THEN CAST(1 AS bit)
ELSE CAST(0 AS bit)
END
fail: Microsoft.EntityFrameworkCore.Database.Command[20102]
Failed executing DbCommand (5ms) [Parameters=[@p0='?' (DbType = Int32), @p1='?' (Size = 4000), @p2='?' (DbType = DateTime2), @p3='?' (DbType = DateTime2), @p4='?' (Size = 4000), @p5='?' (DbType = Int32), @p6='?' (DbType = Double),
@p7='?' (DbType = Double), @p8='?' (Size = 4000), @p9='?' (Size = 8000) (DbType = Binary), @p10='?' (Size = 8000) (DbType = Binary), @p11='?' (Size = 4000)], CommandType='Text', CommandTimeout='30']
SET IMPLICIT_TRANSACTIONS OFF;
SET NOCOUNT ON;
INSERT INTO [Usuario] ([AssociadoIdAssociado], [Cpf], [DtAcesso], [DtCadastro], [Email], [IdAssociado], [Latitude], [Longitude], [NomeUsuario], [PasswordHash], [PasswordSalt], [TpUsuario])
OUTPUT INSERTED.[IdUsuario]
VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11);
fail: Microsoft.EntityFrameworkCore.Update[10000]
An exception occurred in the database while saving changes for context type 'QuantoDemoraApi.Data.DataContext'.
Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
---> Microsoft.Data.SqlClient.SqlException (0x80131904): Nome de coluna 'AssociadoIdAssociado' inválido.
at Microsoft.Data.SqlClient.SqlCommand.<>c.<ExecuteDbDataReaderAsync>b**208_0(Task`1 result)
         at System.Threading.Tasks.ContinuationResultTaskFromResultTask`2.InnerInvoke()
at System.Threading.Tasks.Task.<>c.<.cctor>b**272_0(Object obj)
at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location ---
at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot, Thread threadPoolThread)
--- End of stack trace from previous location ---
at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
ClientConnectionId:3ca849e3-7474-4279-9658-00ef25e20f68
Error Number:207,State:1,Class:16
--- End of inner exception stack trace ---
at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.SqlServer.Update.Internal.SqlServerModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(IList`1 entriesToSave, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(StateManager stateManager, Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
Microsoft.EntityFrameworkCore.DbUpdateException: An error occurred while saving the entity changes. See the inner exception for details.
---> Microsoft.Data.SqlClient.SqlException (0x80131904): Nome de coluna 'AssociadoIdAssociado' inválido.
at Microsoft.Data.SqlClient.SqlCommand.<>c.<ExecuteDbDataReaderAsync>b**208_0(Task`1 result)
         at System.Threading.Tasks.ContinuationResultTaskFromResultTask`2.InnerInvoke()
at System.Threading.Tasks.Task.<>c.<.cctor>b\_\_272_0(Object obj)
at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location ---
at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot, Thread threadPoolThread)
--- End of stack trace from previous location ---
at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.Storage.RelationalCommand.ExecuteReaderAsync(RelationalCommandParameterObject parameterObject, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
ClientConnectionId:3ca849e3-7474-4279-9658-00ef25e20f68
Error Number:207,State:1,Class:16
--- End of inner exception stack trace ---
at Microsoft.EntityFrameworkCore.Update.ReaderModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.SqlServer.Update.Internal.SqlServerModificationCommandBatch.ExecuteAsync(IRelationalConnection connection, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.Update.Internal.BatchExecutor.ExecuteAsync(IEnumerable`1 commandBatches, IRelationalConnection connection, CancellationToken cancellationToken)
         at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(IList`1 entriesToSave, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.ChangeTracking.Internal.StateManager.SaveChangesAsync(StateManager stateManager, Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal.SqlServerExecutionStrategy.ExecuteAsync[TState,TResult](TState state, Func`4 operation, Func`4 verifySucceeded, CancellationToken cancellationToken)
at Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken)
