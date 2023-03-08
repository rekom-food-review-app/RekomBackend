using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RekomBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateRatingResultView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql(@"
            create definer = root@`%` view RekomDbPro.RatingResultViews as
            select `Res`.`Id`                                                                  AS `RestaurantId`,
                   ifnull((select (sum(`R2`.`Point`) / count(`Rev`.`Id`))
                           from ((`RekomDbPro`.`Reviews` `Rev` join `RekomDbPro`.`Restaurants` `R`
                                  on ((`R`.`Id` = `Rev`.`RestaurantId`))) join `RekomDbPro`.`Ratings` `R2`
                                 on ((`R2`.`Id` = `Rev`.`RatingId`)))
                           where (`R`.`Id` = `Res`.`Id`)), 0)                                  AS `Average`,
                   ifnull((select count(`Rev`.`Id`)
                           from ((`RekomDbPro`.`Reviews` `Rev` join `RekomDbPro`.`Restaurants` `R`
                                  on ((`R`.`Id` = `Rev`.`RestaurantId`))) join `RekomDbPro`.`Ratings` `R2`
                                 on ((`R2`.`Id` = `Rev`.`RatingId`)))
                           where (`R`.`Id` = `Res`.`Id`)), 0)                                  AS `Amount`,
                   ifnull(((select (count(`Rev`.`Id`) / `Amount`)
                            from ((`RekomDbPro`.`Reviews` `Rev` join `RekomDbPro`.`Restaurants` `R`
                                   on ((`R`.`Id` = `Rev`.`RestaurantId`))) join `RekomDbPro`.`Ratings` `R2`
                                  on ((`R2`.`Id` = `Rev`.`RatingId`)))
                            where ((`R`.`Id` = `Res`.`Id`) and (`R2`.`Point` = 5))) * 100), 0) AS `PercentFive`,
                   ifnull(((select (count(`Rev`.`Id`) / `Amount`)
                            from ((`RekomDbPro`.`Reviews` `Rev` join `RekomDbPro`.`Restaurants` `R`
                                   on ((`R`.`Id` = `Rev`.`RestaurantId`))) join `RekomDbPro`.`Ratings` `R2`
                                  on ((`R2`.`Id` = `Rev`.`RatingId`)))
                            where ((`R`.`Id` = `Res`.`Id`) and (`R2`.`Point` = 4))) * 100), 0) AS `PercentFour`,
                   ifnull(((select (count(`Rev`.`Id`) / `Amount`)
                            from ((`RekomDbPro`.`Reviews` `Rev` join `RekomDbPro`.`Restaurants` `R`
                                   on ((`R`.`Id` = `Rev`.`RestaurantId`))) join `RekomDbPro`.`Ratings` `R2`
                                  on ((`R2`.`Id` = `Rev`.`RatingId`)))
                            where ((`R`.`Id` = `Res`.`Id`) and (`R2`.`Point` = 3))) * 100), 0) AS `PercentThree`,
                   ifnull(((select (count(`Rev`.`Id`) / `Amount`)
                            from ((`RekomDbPro`.`Reviews` `Rev` join `RekomDbPro`.`Restaurants` `R`
                                   on ((`R`.`Id` = `Rev`.`RestaurantId`))) join `RekomDbPro`.`Ratings` `R2`
                                  on ((`R2`.`Id` = `Rev`.`RatingId`)))
                            where ((`R`.`Id` = `Res`.`Id`) and (`R2`.`Point` = 2))) * 100), 0) AS `PercentTwo`,
                   ifnull(((select (count(`Rev`.`Id`) / `Amount`)
                            from ((`RekomDbPro`.`Reviews` `Rev` join `RekomDbPro`.`Restaurants` `R`
                                   on ((`R`.`Id` = `Rev`.`RestaurantId`))) join `RekomDbPro`.`Ratings` `R2`
                                  on ((`R2`.`Id` = `Rev`.`RatingId`)))
                            where ((`R`.`Id` = `Res`.`Id`) and (`R2`.`Point` = 1))) * 100), 0) AS `PercentOne`
            from `RekomDbPro`.`Restaurants` `Res`;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
