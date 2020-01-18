using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Addresses.Migrations
{
    public partial class DataGenerator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO addresses.""Countries"" VALUES(1, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(2, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(3, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(4, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(5, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(6, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(7, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(8, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(9, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(10, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(11, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(12, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(13, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(14, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(15, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(16, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(17, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(18, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(19, 'Albania', 'AL', 'ALB');
INSERT INTO addresses.""Countries"" VALUES(20, 'Albania', 'AL', 'ALB');


INSERT INTO addresses.""Regions"" VALUES(1, 'RegionName1', 1);
INSERT INTO addresses.""Regions"" VALUES(2, 'RegionName2', 2);
INSERT INTO addresses.""Regions"" VALUES(3, 'RegionName3', 3);
INSERT INTO addresses.""Regions"" VALUES(4, 'RegionName4', 4);
INSERT INTO addresses.""Regions"" VALUES(5, 'RegionName5', 5);
INSERT INTO addresses.""Regions"" VALUES(6, 'RegionName6', 6);
INSERT INTO addresses.""Regions"" VALUES(7, 'RegionName7', 7);
INSERT INTO addresses.""Regions"" VALUES(8, 'RegionName8', 8);
INSERT INTO addresses.""Regions"" VALUES(9, 'RegionName9', 9);
INSERT INTO addresses.""Regions"" VALUES(10, 'RegionName10', 10);
INSERT INTO addresses.""Regions"" VALUES(11, 'RegionName11', 11);
INSERT INTO addresses.""Regions"" VALUES(12, 'RegionName12', 12);
INSERT INTO addresses.""Regions"" VALUES(13, 'RegionName13', 13);
INSERT INTO addresses.""Regions"" VALUES(14, 'RegionName14', 14);
INSERT INTO addresses.""Regions"" VALUES(15, 'RegionName15', 15);
INSERT INTO addresses.""Regions"" VALUES(16, 'RegionName16', 16);
INSERT INTO addresses.""Regions"" VALUES(17, 'RegionName17', 17);
INSERT INTO addresses.""Regions"" VALUES(18, 'RegionName18', 18);
INSERT INTO addresses.""Regions"" VALUES(19, 'RegionName19', 19);
INSERT INTO addresses.""Regions"" VALUES(20, 'RegionName20', 20);


INSERT INTO addresses.""LocalityTypes"" VALUES(1, 'LocalityName1');
INSERT INTO addresses.""LocalityTypes"" VALUES(2, 'LocalityName2');
INSERT INTO addresses.""LocalityTypes"" VALUES(3, 'LocalityName3');
INSERT INTO addresses.""LocalityTypes"" VALUES(4, 'LocalityName4');
INSERT INTO addresses.""LocalityTypes"" VALUES(5, 'LocalityName5');
INSERT INTO addresses.""LocalityTypes"" VALUES(6, 'LocalityName6');
INSERT INTO addresses.""LocalityTypes"" VALUES(7, 'LocalityName7');
INSERT INTO addresses.""LocalityTypes"" VALUES(8, 'LocalityName8');
INSERT INTO addresses.""LocalityTypes"" VALUES(9, 'LocalityName9');
INSERT INTO addresses.""LocalityTypes"" VALUES(10, 'LocalityName10');
INSERT INTO addresses.""LocalityTypes"" VALUES(11, 'LocalityName11');
INSERT INTO addresses.""LocalityTypes"" VALUES(12, 'LocalityName12');
INSERT INTO addresses.""LocalityTypes"" VALUES(13, 'LocalityName13');
INSERT INTO addresses.""LocalityTypes"" VALUES(14, 'LocalityName14');
INSERT INTO addresses.""LocalityTypes"" VALUES(15, 'LocalityName15');
INSERT INTO addresses.""LocalityTypes"" VALUES(16, 'LocalityName16');
INSERT INTO addresses.""LocalityTypes"" VALUES(17, 'LocalityName17');
INSERT INTO addresses.""LocalityTypes"" VALUES(18, 'LocalityName18');
INSERT INTO addresses.""LocalityTypes"" VALUES(19, 'LocalityName19');
INSERT INTO addresses.""LocalityTypes"" VALUES(20, 'LocalityName20');


INSERT INTO addresses.""Localities"" VALUES(1, 'Name1', 1, 1, 1);
INSERT INTO addresses.""Localities"" VALUES(2, 'Name2', 2, 2, 2);
INSERT INTO addresses.""Localities"" VALUES(3, 'Name3', 3, 3, 3);
INSERT INTO addresses.""Localities"" VALUES(4, 'Name4', 4, 4, 4);
INSERT INTO addresses.""Localities"" VALUES(5, 'Name5', 5, 5, 5);
INSERT INTO addresses.""Localities"" VALUES(6, 'Name6', 6, 6, 6);
INSERT INTO addresses.""Localities"" VALUES(7, 'Name7', 7, 7, 7);
INSERT INTO addresses.""Localities"" VALUES(8, 'Name8', 8, 8, 8);
INSERT INTO addresses.""Localities"" VALUES(9, 'Name9', 9, 9, 9);
INSERT INTO addresses.""Localities"" VALUES(10, 'Name10', 10, 10, 10);
INSERT INTO addresses.""Localities"" VALUES(11, 'Name11', 11, 11, 11);
INSERT INTO addresses.""Localities"" VALUES(12, 'Name12', 12, 12, 12);
INSERT INTO addresses.""Localities"" VALUES(13, 'Name13', 13, 13, 13);
INSERT INTO addresses.""Localities"" VALUES(14, 'Name14', 14, 14, 14);
INSERT INTO addresses.""Localities"" VALUES(15, 'Name15', 15, 15, 15);
INSERT INTO addresses.""Localities"" VALUES(16, 'Name16', 16, 16, 16);
INSERT INTO addresses.""Localities"" VALUES(17, 'Name17', 17, 17, 17);
INSERT INTO addresses.""Localities"" VALUES(18, 'Name18', 18, 18, 18);
INSERT INTO addresses.""Localities"" VALUES(19, 'Name19', 19, 19, 19);
INSERT INTO addresses.""Localities"" VALUES(20, 'Name20', 20, 20, 20);


INSERT INTO addresses.""Districts"" VALUES(1, 'Name1', 1);
INSERT INTO addresses.""Districts"" VALUES(2, 'Name2', 2);
INSERT INTO addresses.""Districts"" VALUES(3, 'Name3', 3);
INSERT INTO addresses.""Districts"" VALUES(4, 'Name4', 4);
INSERT INTO addresses.""Districts"" VALUES(5, 'Name5', 5);
INSERT INTO addresses.""Districts"" VALUES(6, 'Name6', 6);
INSERT INTO addresses.""Districts"" VALUES(7, 'Name7', 7);
INSERT INTO addresses.""Districts"" VALUES(8, 'Name8', 8);
INSERT INTO addresses.""Districts"" VALUES(9, 'Name9', 9);
INSERT INTO addresses.""Districts"" VALUES(10, 'Name10', 10);
INSERT INTO addresses.""Districts"" VALUES(11, 'Name11', 11);
INSERT INTO addresses.""Districts"" VALUES(12, 'Name12', 12);
INSERT INTO addresses.""Districts"" VALUES(13, 'Name13', 13);
INSERT INTO addresses.""Districts"" VALUES(14, 'Name14', 14);
INSERT INTO addresses.""Districts"" VALUES(15, 'Name15', 15);
INSERT INTO addresses.""Districts"" VALUES(16, 'Name16', 16);
INSERT INTO addresses.""Districts"" VALUES(17, 'Name17', 17);
INSERT INTO addresses.""Districts"" VALUES(18, 'Name18', 18);
INSERT INTO addresses.""Districts"" VALUES(19, 'Name19', 19);
INSERT INTO addresses.""Districts"" VALUES(20, 'Name20', 20);


INSERT INTO addresses.""StreetTypes"" VALUES(1, 'Name1');
INSERT INTO addresses.""StreetTypes"" VALUES(2, 'Name2');
INSERT INTO addresses.""StreetTypes"" VALUES(3, 'Name3');
INSERT INTO addresses.""StreetTypes"" VALUES(4, 'Name4');
INSERT INTO addresses.""StreetTypes"" VALUES(5, 'Name5');
INSERT INTO addresses.""StreetTypes"" VALUES(6, 'Name6');
INSERT INTO addresses.""StreetTypes"" VALUES(7, 'Name7');
INSERT INTO addresses.""StreetTypes"" VALUES(8, 'Name8');
INSERT INTO addresses.""StreetTypes"" VALUES(9, 'Name9');
INSERT INTO addresses.""StreetTypes"" VALUES(10, 'Name10');
INSERT INTO addresses.""StreetTypes"" VALUES(11, 'Name11');
INSERT INTO addresses.""StreetTypes"" VALUES(12, 'Name12');
INSERT INTO addresses.""StreetTypes"" VALUES(13, 'Name13');
INSERT INTO addresses.""StreetTypes"" VALUES(14, 'Name14');
INSERT INTO addresses.""StreetTypes"" VALUES(15, 'Name15');
INSERT INTO addresses.""StreetTypes"" VALUES(16, 'Name16');
INSERT INTO addresses.""StreetTypes"" VALUES(17, 'Name17');
INSERT INTO addresses.""StreetTypes"" VALUES(18, 'Name18');
INSERT INTO addresses.""StreetTypes"" VALUES(19, 'Name19');
INSERT INTO addresses.""StreetTypes"" VALUES(20, 'Name20');


INSERT INTO addresses.""Streets"" VALUES(1, 'Street1', 1, 1);
INSERT INTO addresses.""Streets"" VALUES(2, 'Street2', 2, 2);
INSERT INTO addresses.""Streets"" VALUES(3, 'Street3', 3, 3);
INSERT INTO addresses.""Streets"" VALUES(4, 'Street4', 4, 4);
INSERT INTO addresses.""Streets"" VALUES(5, 'Street5', 5, 5);
INSERT INTO addresses.""Streets"" VALUES(6, 'Street6', 6, 6);
INSERT INTO addresses.""Streets"" VALUES(7, 'Street7', 7, 7);
INSERT INTO addresses.""Streets"" VALUES(8, 'Street8', 8, 8);
INSERT INTO addresses.""Streets"" VALUES(9, 'Street9', 9, 9);
INSERT INTO addresses.""Streets"" VALUES(10, 'Street10', 10, 10);
INSERT INTO addresses.""Streets"" VALUES(11, 'Street11', 11, 11);
INSERT INTO addresses.""Streets"" VALUES(12, 'Street12', 12, 12);
INSERT INTO addresses.""Streets"" VALUES(13, 'Street13', 13, 13);
INSERT INTO addresses.""Streets"" VALUES(14, 'Street14', 14, 14);
INSERT INTO addresses.""Streets"" VALUES(15, 'Street15', 15, 15);
INSERT INTO addresses.""Streets"" VALUES(16, 'Street16', 16, 16);
INSERT INTO addresses.""Streets"" VALUES(17, 'Street17', 17, 17);
INSERT INTO addresses.""Streets"" VALUES(18, 'Street18', 18, 18);
INSERT INTO addresses.""Streets"" VALUES(19, 'Street19', 19, 19);
INSERT INTO addresses.""Streets"" VALUES(20, 'Street20', 20, 20);


INSERT INTO addresses.""Addresses"" VALUES(1, 1, 1, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(2, 2, 2, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(3, 3, 3, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(4, 4, 4, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(5, 5, 5, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(6, 6, 6, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(7, 7, 7, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(8, 8, 8, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(9, 9, 9, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(10, 10, 10, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(11, 11, 11, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(12, 12, 12, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(13, 13, 13, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(14, 14, 14, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(15, 15, 15, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(16, 16, 16, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(17, 17, 17, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(18, 18, 18, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(19, 19, 19, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');
INSERT INTO addresses.""Addresses"" VALUES(20, 20, 20, 'House', 'HouseBlock', 'Building', 'HomeOwnership', 'Ownership', 'Apartment', 117142, 2, 'Intercom', 4, 'Pavilion', 48.8534100, 2.3488000, 'Some_comment');");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM addresses.""Countries"" * 
DELETE FROM addresses.""Regions"" * 
DELETE FROM addresses.""LocalityTypes"" * 
DELETE FROM addresses.""Localities"" * 
DELETE FROM addresses.""Districts"" * 
DELETE FROM addresses.""StreetTypes"" * 
DELETE FROM addresses.""Streets"" * 
DELETE FROM addresses.""Addresses"" * ");
        }
    }
}
