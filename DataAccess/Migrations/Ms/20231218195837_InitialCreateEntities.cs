using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.Ms
{
    /// <inheritdoc />
    public partial class InitialCreateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CinsiyetId",
                table: "Ogrenci");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DogumTarihi",
                table: "Ogrenci",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Cinsiyet",
                table: "Ogrenci",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Danismanlik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OgrElmID = table.Column<int>(type: "int", nullable: false),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danismanlik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Degerlendirme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinavId = table.Column<int>(type: "int", nullable: false),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    SinavNotu = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degerlendirme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DersAcma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AkademikYilId = table.Column<int>(type: "int", nullable: false),
                    AkademikDonemId = table.Column<int>(type: "int", nullable: false),
                    MufredatId = table.Column<int>(type: "int", nullable: false),
                    OgrElmId = table.Column<int>(type: "int", nullable: false),
                    Kontenjan = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersAcma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DersAlma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAcmaId = table.Column<int>(type: "int", nullable: false),
                    OgrenciId = table.Column<int>(type: "int", nullable: false),
                    DersDurumId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersAlma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DersHavuzu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersDiliId = table.Column<int>(type: "int", nullable: false),
                    DersSeviyesiId = table.Column<int>(type: "int", nullable: false),
                    DersturuId = table.Column<int>(type: "int", nullable: false),
                    DersKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DersAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teorik = table.Column<int>(type: "int", nullable: false),
                    Uygulama = table.Column<int>(type: "int", nullable: false),
                    Kredi = table.Column<float>(type: "real", nullable: false),
                    ECTS = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersHavuzu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Derslik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DerslikTuruId = table.Column<int>(type: "int", nullable: false),
                    DerslikAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kapasite = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Derslik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DersProgrami",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAcmaId = table.Column<int>(type: "int", nullable: false),
                    DerslikId = table.Column<int>(type: "int", nullable: false),
                    DersGunuId = table.Column<int>(type: "int", nullable: false),
                    DersSaati = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersProgrami", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mufredat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: false),
                    AkedemikYilId = table.Column<int>(type: "int", nullable: false),
                    AkedemikDonemId = table.Column<int>(type: "int", nullable: false),
                    DersDonemi = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mufredat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OgretimElemani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    KurumSicilNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unvan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyadi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCKimlikNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cinsiyet = table.Column<bool>(type: "bit", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgretimElemani", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sinav",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAcmaId = table.Column<int>(type: "int", nullable: false),
                    SinavTuruId = table.Column<int>(type: "int", nullable: false),
                    DerslikId = table.Column<int>(type: "int", nullable: false),
                    OgrElmID = table.Column<int>(type: "int", nullable: false),
                    EtkiOrani = table.Column<int>(type: "int", nullable: false),
                    SinavTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinav", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_AkademikDonem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_AkademikDonem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_AkademikYil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_AkademikYil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_DersAlmaDurumu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_DersAlmaDurumu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_DersDili",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_DersDili", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_DersGunu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_DersGunu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_DerslikTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_DerslikTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_DersSeviyesi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_DersSeviyesi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_DersTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_DersTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_OgrenciDurum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_OgrenciDurum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_OgretimDili",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_OgretimDili", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_OgretimTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_OgretimTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_ProgramTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_ProgramTuru", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ST_SinavTuru",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ekstra = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ST_SinavTuru", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Danismanlik");

            migrationBuilder.DropTable(
                name: "Degerlendirme");

            migrationBuilder.DropTable(
                name: "DersAcma");

            migrationBuilder.DropTable(
                name: "DersAlma");

            migrationBuilder.DropTable(
                name: "DersHavuzu");

            migrationBuilder.DropTable(
                name: "Derslik");

            migrationBuilder.DropTable(
                name: "DersProgrami");

            migrationBuilder.DropTable(
                name: "Mufredat");

            migrationBuilder.DropTable(
                name: "OgretimElemani");

            migrationBuilder.DropTable(
                name: "Sinav");

            migrationBuilder.DropTable(
                name: "ST_AkademikDonem");

            migrationBuilder.DropTable(
                name: "ST_AkademikYil");

            migrationBuilder.DropTable(
                name: "ST_DersAlmaDurumu");

            migrationBuilder.DropTable(
                name: "ST_DersDili");

            migrationBuilder.DropTable(
                name: "ST_DersGunu");

            migrationBuilder.DropTable(
                name: "ST_DerslikTuru");

            migrationBuilder.DropTable(
                name: "ST_DersSeviyesi");

            migrationBuilder.DropTable(
                name: "ST_DersTuru");

            migrationBuilder.DropTable(
                name: "ST_OgrenciDurum");

            migrationBuilder.DropTable(
                name: "ST_OgretimDili");

            migrationBuilder.DropTable(
                name: "ST_OgretimTuru");

            migrationBuilder.DropTable(
                name: "ST_ProgramTuru");

            migrationBuilder.DropTable(
                name: "ST_SinavTuru");

            migrationBuilder.DropColumn(
                name: "Cinsiyet",
                table: "Ogrenci");

            migrationBuilder.AlterColumn<string>(
                name: "DogumTarihi",
                table: "Ogrenci",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "CinsiyetId",
                table: "Ogrenci",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
