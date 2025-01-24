# Desbravador
O aplicativo de cadastro de projetos para uma construtora, foi desenvolvido com .NET 8 e React.js, com um banco de dados local Postgres.
## Usabilidade
Foi desenvolvido um SPA simples, com um menu superior, podendo ser navegável entre as telas de Projetos e Funcionários.
### Funcionários
A primeira vez que acessar a tela de Funcionários, irão ser cadastrados 5 novos funcionários vindos da API aleatória https://randomuser.me/api/. Após isso, para inserir funcionários, existe um botão 'Inserir Aleatórios', que irá inserir de 5 novos funcionários, conforme clicado.

Na grid de funcionários, possui 2 botões, para excluir(se o funcionário estiver como Responsável por algum projeto, não será possível excluir) e para visualizar detalhes, nesse botão abrirá um modal, com mais informações, dentre elas os projetos que o funcionário é Responsável e os projetos que ele está Vinculado.

### Projetos
Na tela de projetos temos um botão para inserir novos Projetos, com todos os campos requisitados.
Na grid de projetos, há 4 botões(Visualizar Detalhes, Vincular Funcionários, Editar, Excluir):
* O botão de visualizar detalhes abrirá uma modal, com todas as informações do projeto, principalmente os funcionários que estão vinculados ao projeto.
* No botão de Vincular Funcionários, abrirá uma modal, com um select possível de selecionar funcionários, com um botão '+', para incluí-los. Pode ser selecionado vários, e irão ser carregados numa grid abaixo. Ao selecionar todos os funcionários, apertar no botão 'Vincular'. Dessa forma, já irá ser feito o vínculo de Funcionários no Projeto.
* No botão de Editar, podem ser alteradas todas as informações do projeto.
* No botão de excluir, poderá ser excluído o projeto, desde que não esteja nos status Iniciado, Em Andamento e Encerrado.

## Como usar
### API
* Na appsettings da API, a connectionString é um banco de dados local, Postgres. Apenas ajustar usuário e senha do seu banco.
* Pode ser rodado um comando no *Package Manager Console*, Update-Database, para executar a Migration e criar o banco de dados.
* A porta para execução está na "https://localhost:7054. Api também está com suporte ao Swagger, para visualizar endpoints ou rodar.

### SPA
* SPA está na porta padrão https://localhost:3000.
* Foram instalados alguns pacotes com o npm, podendo ser visualizados nas dependencies do package.json.
* Rodar com npm start, após dependencias instaladas. 
