using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using NpgsqlTypes;

#nullable disable

namespace RekomBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:role", "admin,rekomer,restaurant")
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(500)", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    Point = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    Point = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "varchar(4)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AccountId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Otps_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rekomers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "varchar(200)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "varchar(200)", nullable: false),
                    Dob = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    FullTextSearch = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false),
                    AccountId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rekomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rekomers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "varchar(200)", nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", nullable: false),
                    Location = table.Column<Point>(type: "geography(Point, 4326)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    FullTextSearch = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false)
                        .Annotation("Npgsql:TsVectorConfig", "english")
                        .Annotation("Npgsql:TsVectorProperties", new[] { "Name", "Description", "Address" }),
                    AccountId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FollowerId = table.Column<string>(type: "text", nullable: false),
                    FollowingId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follows_Rekomers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Rekomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Follows_Rekomers_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "Rekomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteRestaurants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RekomerId = table.Column<string>(type: "text", nullable: false),
                    RestaurantId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteRestaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteRestaurants_Rekomers_RekomerId",
                        column: x => x.RekomerId,
                        principalTable: "Rekomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteRestaurants_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(200)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: true),
                    FullTextSearch = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false),
                    RestaurantId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "varchar(500)", nullable: false),
                    AmountAgree = table.Column<long>(type: "bigint", nullable: false),
                    AmountDisagree = table.Column<long>(type: "bigint", nullable: false),
                    AmountUseful = table.Column<long>(type: "bigint", nullable: false),
                    AmountReply = table.Column<long>(type: "bigint", nullable: false),
                    RestaurantId = table.Column<string>(type: "text", nullable: false),
                    RekomerId = table.Column<string>(type: "text", nullable: false),
                    RatingId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Rekomers_RekomerId",
                        column: x => x.RekomerId,
                        principalTable: "Rekomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodImage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(200)", nullable: false),
                    FoodId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodImage_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ReviewId = table.Column<string>(type: "text", nullable: false),
                    RekomerId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Rekomers_RekomerId",
                        column: x => x.RekomerId,
                        principalTable: "Rekomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewMedias",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MediaUrl = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ReviewId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewMedias_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewReactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ReviewId = table.Column<string>(type: "text", nullable: false),
                    RekomerId = table.Column<string>(type: "text", nullable: false),
                    ReactionId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewReactions_Reactions_ReactionId",
                        column: x => x.ReactionId,
                        principalTable: "Reactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewReactions_Rekomers_RekomerId",
                        column: x => x.RekomerId,
                        principalTable: "Rekomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewReactions_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RekomerId",
                table: "Comments",
                column: "RekomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReviewId",
                table: "Comments",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteRestaurants_RekomerId_RestaurantId",
                table: "FavouriteRestaurants",
                columns: new[] { "RekomerId", "RestaurantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteRestaurants_RestaurantId",
                table: "FavouriteRestaurants",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId_FollowingId",
                table: "Follows",
                columns: new[] { "FollowerId", "FollowingId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowingId",
                table: "Follows",
                column: "FollowingId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodImage_FoodId",
                table: "FoodImage",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FullTextSearch",
                table: "Foods",
                column: "FullTextSearch")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_RestaurantId",
                table: "Foods",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Otps_AccountId",
                table: "Otps",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Tag",
                table: "Ratings",
                column: "Tag");

            migrationBuilder.CreateIndex(
                name: "IX_Rekomers_AccountId",
                table: "Rekomers",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rekomers_FullTextSearch",
                table: "Rekomers",
                column: "FullTextSearch")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_AccountId",
                table: "Restaurants",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_FullTextSearch",
                table: "Restaurants",
                column: "FullTextSearch")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_Location",
                table: "Restaurants",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewMedias_ReviewId",
                table: "ReviewMedias",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReactions_ReactionId",
                table: "ReviewReactions",
                column: "ReactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReactions_RekomerId",
                table: "ReviewReactions",
                column: "RekomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReactions_ReviewId",
                table: "ReviewReactions",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RatingId",
                table: "Reviews",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RekomerId",
                table: "Reviews",
                column: "RekomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews",
                column: "RestaurantId");
            
                       
             migrationBuilder.Sql(@"
                CREATE VIEW ""RatingResultViews""
                as
                select
                ""Rev"".""RestaurantId"" as ""RestaurantId"",
                ROUND(avg(R2.""Point""), 1) as ""Average"",
                count(*) as ""Amount"",
                ROUND(COUNT(CASE WHEN R2.""Point"" = 5 THEN 1 END) * 100.0 / COUNT(*), 1) AS ""PercentFive"",
                ROUND(COUNT(CASE WHEN R2.""Point"" = 4 THEN 1 END) * 100.0 / COUNT(*), 1) AS ""PercentFour"",
                ROUND(COUNT(CASE WHEN R2.""Point"" = 3 THEN 1 END) * 100.0 / COUNT(*), 1) AS ""PercentThree"",
                ROUND(COUNT(CASE WHEN R2.""Point"" = 2 THEN 1 END) * 100.0 / COUNT(*), 1) AS ""PercentTwo"",
                ROUND(COUNT(CASE WHEN R2.""Point"" = 1 THEN 1 END) * 100.0 / COUNT(*), 1) AS ""PercentOne""
                from ""Reviews"" ""Rev""
                inner join ""Ratings"" R2 on R2.""Id"" = ""Rev"".""RatingId""
                group by ""Rev"".""RestaurantId"";
                ");

            migrationBuilder.Sql(@"
                INSERT INTO public.""Ratings"" (""Id"", ""Tag"", ""Point"", ""CreatedAt"", ""UpdatedAt"")
                VALUES ('1'::text, 'kem'::text, 1::bigint, '2023-03-08 22:04:26.000000'::timestamp,
                    '2023-03-08 22:04:27.000000'::timestamp);

                INSERT INTO public.""Ratings"" (""Id"", ""Tag"", ""Point"", ""CreatedAt"", ""UpdatedAt"")
                VALUES ('2'::text, 'yeu'::text, 2::bigint, '2023-03-08 22:04:30.000000'::timestamp,
                '2023-03-08 22:04:31.000000'::timestamp);

                INSERT INTO public.""Ratings"" (""Id"", ""Tag"", ""Point"", ""CreatedAt"", ""UpdatedAt"")
                VALUES ('3'::text, 'trung-binh'::text, 3::bigint, '2023-03-08 22:04:32.000000'::timestamp,
                '2023-03-08 22:04:33.000000'::timestamp);

                INSERT INTO public.""Ratings"" (""Id"", ""Tag"", ""Point"", ""CreatedAt"", ""UpdatedAt"")
                VALUES ('4'::text, 'kha'::text, 4::bigint, '2023-03-08 22:04:35.000000'::timestamp,
                '2023-03-08 22:04:36.000000'::timestamp);

                INSERT INTO public.""Ratings"" (""Id"", ""Tag"", ""Point"", ""CreatedAt"", ""UpdatedAt"")
                VALUES ('5'::text, 'tot'::text, 5::bigint, '2023-03-08 22:04:37.000000'::timestamp,
                '2023-03-08 22:04:37.000000'::timestamp);
                ");

            migrationBuilder.Sql(@"
                INSERT INTO public.""Reactions"" (""Id"", ""Tag"", ""Point"", ""CreatedAt"", ""UpdatedAt"")
                VALUES ('1'::text, 'agree'::text, 2::bigint, '2023-03-08 22:06:09.000000'::timestamp,
                    '2023-03-08 22:06:12.000000'::timestamp);

                INSERT INTO public.""Reactions"" (""Id"", ""Tag"", ""Point"", ""CreatedAt"", ""UpdatedAt"")
                VALUES ('2'::text, 'disagree'::text, 0::bigint, '2023-03-08 22:06:15.000000'::timestamp,
                '2023-03-08 22:06:16.000000'::timestamp);

                INSERT INTO public.""Reactions"" (""Id"", ""Tag"", ""Point"", ""CreatedAt"", ""UpdatedAt"")
                VALUES ('3'::text, 'useful'::text, 1::bigint, '2023-03-08 22:06:16.000000'::timestamp,
                '2023-03-08 22:06:17.000000'::timestamp);
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FavouriteRestaurants");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "FoodImage");

            migrationBuilder.DropTable(
                name: "Otps");

            migrationBuilder.DropTable(
                name: "ReviewMedias");

            migrationBuilder.DropTable(
                name: "ReviewReactions");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Rekomers");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
