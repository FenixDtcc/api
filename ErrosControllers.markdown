http://localhost:5188/Especialidades/GetAll
Retorno: 400 BadRequest
The dependent side could not be determined for the one-to-one relationship between 'Associado.Usuario' and 'Usuario.Associado'. To identify the dependent side of the relationship, configure the foreign key property. If these navigations should not be part of the same relationship, configure them independently via separate method chains in 'OnModelCreating'. See http://go.microsoft.com/fwlink/?LinkId=724062 for more details.

Exclui a referencia ao Usuario que estava na classe Associado, porém, continua dando o erro abaixo:
The entity type 'Associado' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'. For more information on keyless entity types, see https://go.microsoft.com/fwlink/?linkid=2141943.
