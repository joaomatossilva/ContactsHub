CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "AspNetRoles" (
    "Id" TEXT NOT NULL,
    "Name" TEXT NULL,
    "NormalizedName" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetUsers" (
    "Id" TEXT NOT NULL,
    "UserName" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "Email" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "AccessFailedCount" INTEGER NOT NULL,
    CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id")
);

CREATE TABLE "AspNetRoleClaims" (
    "Id" INTEGER NOT NULL,
    "RoleId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserClaims" (
    "Id" INTEGER NOT NULL,
    "UserId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserRoles" (
    "UserId" TEXT NOT NULL,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserTokens" (
    "UserId" TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('00000000000000_CreateIdentitySchema', '6.0.10');

COMMIT;

START TRANSACTION;

CREATE TABLE "Contacts" (
    "Id" TEXT NOT NULL,
    "UserId" TEXT NOT NULL,
    "Type" INTEGER NOT NULL,
    "Name" TEXT NULL,
    "Value" TEXT NOT NULL,
    CONSTRAINT "PK_Contacts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Contacts_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Contacts_UserId" ON "Contacts" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221012171444_Contacts', '6.0.10');

COMMIT;

START TRANSACTION;

CREATE TABLE "Friends" (
    "Id" TEXT NOT NULL,
    "UserId" TEXT NOT NULL,
    "FriendUserId" TEXT NOT NULL,
    CONSTRAINT "PK_Friends" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Friends_AspNetUsers_FriendUserId" FOREIGN KEY ("FriendUserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Friends_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Friends_FriendUserId" ON "Friends" ("FriendUserId");

CREATE INDEX "IX_Friends_UserId" ON "Friends" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221018090236_Friends', '6.0.10');

COMMIT;

START TRANSACTION;

CREATE TABLE "FriendRequests" (
    "Id" TEXT NOT NULL,
    "FromUserId" TEXT NOT NULL,
    "ToUserId" TEXT NOT NULL,
    "DateTimeUtc" TEXT NOT NULL,
    "State" INTEGER NOT NULL,
    CONSTRAINT "PK_FriendRequests" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_FriendRequests_AspNetUsers_FromUserId" FOREIGN KEY ("FromUserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_FriendRequests_AspNetUsers_ToUserId" FOREIGN KEY ("ToUserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_FriendRequests_FromUserId" ON "FriendRequests" ("FromUserId");

CREATE INDEX "IX_FriendRequests_ToUserId" ON "FriendRequests" ("ToUserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221018134547_FriendRequests', '6.0.10');

COMMIT;

START TRANSACTION;

CREATE TABLE "Codes" (
    "Id" TEXT NOT NULL,
    "Short" TEXT NOT NULL,
    "UserId" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL,
    CONSTRAINT "PK_Codes" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Codes_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_Codes_Short" ON "Codes" ("Short");

CREATE INDEX "IX_Codes_UserId" ON "Codes" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221018144441_Codes', '6.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "AspNetUsers" ADD "Name" TEXT NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221020193925_User Name', '6.0.10');

COMMIT;

---- FIX for Postgres
--START TRANSACTION;
--
-- ALTER TABLE "AspNetUsers"
--     ALTER COLUMN "EmailConfirmed" DROP DEFAULT,
--     ALTER Column "EmailConfirmed" TYPE BOOLEAN USING "EmailConfirmed"::BOOLEAN,
--     ALTER COLUMN "EmailConfirmed" SET DEFAULT FALSE;
--
-- ALTER TABLE "AspNetUsers"
--     ALTER Column "PhoneNumberConfirmed" TYPE Boolean USING
--     CASE WHEN "PhoneNumberConfirmed"=0 THEN FALSE 
--   ELSE TRUE
-- END;
-- 
-- ALTER TABLE "AspNetUsers"
--     ALTER Column "TwoFactorEnabled" TYPE Boolean USING
--         CASE WHEN "TwoFactorEnabled"=0 THEN FALSE
--              ELSE TRUE
--             END;
-- 
-- ALTER TABLE "AspNetUsers"
--     ALTER Column "LockoutEnabled" TYPE Boolean USING
--         CASE WHEN "LockoutEnabled"=0 THEN FALSE
--              ELSE TRUE
--             END;
-- 
-- ALTER TABLE "Codes"
--     ALTER Column "IsActive" TYPE Boolean USING
--         CASE WHEN "IsActive"=0 THEN FALSE
--              ELSE TRUE
--             END;
--
-- ALTER TABLE "AspNetRoleClaims"
--     ALTER Column "Id" ADD GENERATED ALWAYS AS IDENTITY;
--
-- ALTER TABLE "AspNetUserClaims"
--     ALTER Column "Id" ADD GENERATED ALWAYS AS IDENTITY;
--
--COMMIT;