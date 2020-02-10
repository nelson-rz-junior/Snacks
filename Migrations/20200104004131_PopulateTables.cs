using Microsoft.EntityFrameworkCore.Migrations;

namespace Snacks.Migrations
{
    public partial class PopulateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] ([Name],[Description]) VALUES ('Normal', 'Lanche feito com ingredientes normais')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories] ([Name],[Description]) VALUES ('Natural', 'Lanche feito com ingredientes integrais e naturais')");

            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[ImageUrl],[ImageThumbnailUrl],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Cheese Salada','Pão, hamburguer, ovo, presunto, queijo e batata palha','Delicioso pão de hamburguer com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha',12.50,'http://www.macoratti.net/imagens/lanches/cheesesalada1.jpg','http://www.macoratti.net/imagens/lanches/cheesesalada1.jpg',0,1,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[ImageUrl],[ImageThumbnailUrl],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Misto Quente','Pão, presunto, mussarela e tomate','Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho',8.00,'http://www.macoratti.net/imagens/lanches/mistoquente4.jpg','http://www.macoratti.net/imagens/lanches/mistoquente4.jpg',0,1,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[ImageUrl],[ImageThumbnailUrl],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Cheese Burger','Pão, hamburguer, presunto, mussarela e batata pelha','Pão de hamburguer especial com hamburguer de nossa preparação e presunto e mussarela; acompanha batata palha',11.00,'http://www.macoratti.net/imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/imagens/lanches/cheeseburger1.jpg',1,1,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[ImageUrl],[ImageThumbnailUrl],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Peito Peru','Pão integral, queijo branco, peito de peru, cenoura, alface, iogurte','Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iogurte natural',15.00,'http://www.macoratti.net/imagens/lanches/lanchenatural.jpg','http://www.macoratti.net/imagens/lanches/lanchenatural.jpg',1,1,2)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Snacks] ([Name],[SummaryDescription],[FullDescription],[Price],[ImageUrl],[ImageThumbnailUrl],[IsFavoriteSnack],[InStock],[CategoryId]) VALUES ('Atum','Pão integral, queijo branco, atum, cenoura, alface, iogurte','Pão integral natural com queijo branco, atum fresquinho com cenoura ralada e alface picado com cebola e iogurte natural',14.00,'http://www.macoratti.net/imagens/lanches/lanchenatural.jpg','http://www.macoratti.net/imagens/lanches/lanchenatural.jpg',1,1,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Snacks]");
            migrationBuilder.Sql("DELETE FROM [dbo].[Categories]");
        }
    }
}
