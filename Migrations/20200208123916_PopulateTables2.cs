using Microsoft.EntityFrameworkCore.Migrations;

namespace Snacks.Migrations
{
    public partial class PopulateTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] ([Name],[Description]) VALUES ('Category 1', 'Description 1')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] ([Name],[Description]) VALUES ('Category 2', 'Description 2')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] ([Name],[Description]) VALUES ('Category 3', 'Description 3')");

            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Product 1','Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica.','Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos.',140.50,0,0,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Product 2','Ao contrário do que se acredita, Lorem Ipsum não é simplesmente um texto randômico.','Ao contrário do que se acredita, Lorem Ipsum não é simplesmente um texto randômico. Com mais de 2000 anos, suas raízes podem ser encontradas em uma obra de literatura latina clássica datada de 45 AC.',249.00,0,1,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Product 3','O trecho padrão original de Lorem Ipsum, usado desde o século XVI, está reproduzido abaixo.','O trecho padrão original de Lorem Ipsum, usado desde o século XVI, está reproduzido abaixo para os interessados. Seções 1.10.32 e 1.10.33 de \"de Finibus Bonorum et Malorum\" de Cicero também foram reproduzidas abaixo em sua forma exata original.',137.30,1,1,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Product 4','É um fato conhecido de todos que um leitor se distrairá com o conteúdo de texto legível.','É um fato conhecido de todos que um leitor se distrairá com o conteúdo de texto legível de uma página quando estiver examinando sua diagramação.',98.20,1,1,2)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Product 5','Existem muitas variações disponíveis de passagens de Lorem Ipsum.','Existem muitas variações disponíveis de passagens de Lorem Ipsum, mas a maioria sofreu algum tipo de alteração, seja por inserção de passagens com humor, ou palavras aleatórias que não parecem nem um pouco convincentes.',1240.50,1,0,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Categories]");
            migrationBuilder.Sql("DBCC CHECKIDENT ('[dbo].[Categories]', RESEED, 0)");

            migrationBuilder.Sql("DELETE FROM [dbo].[Snacks]");
            migrationBuilder.Sql("DBCC CHECKIDENT ('[dbo].[Snacks]', RESEED, 0)");
        }
    }
}