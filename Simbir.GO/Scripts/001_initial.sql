CREATE TABLE IF NOT EXISTS "Accounts" (
    "Id" serial PRIMARY KEY,
    "Username" text NOT NULL UNIQUE,
    "Password" text NOT NULL,
    "IsAdmin" boolean NOT NULL DEFAULT FALSE,
    "Balance" real NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS "Transport" (
    "Id" serial PRIMARY KEY,
    "OwnerId" serial NOT NULL REFERENCES "Accounts"("Id"),
    "CanBeRented" boolean NOT NULL,
    "TransportType" text NOT NULL,
    "Model" text NOT NULL,
    "Color" text NOT NULL,
    "Identifier" text NOT NULL,
    "Description" text,
    "Latitude" real NOT NULL,
    "Longitude" real NOT NULL,
    "MinutePrice" real,
    "DayPrice" real
);

CREATE TABLE IF NOT EXISTS "Rent" (
    "Id" serial PRIMARY KEY,
    "UserId" serial NOT NULL REFERENCES "Accounts"("Id"),
    "TransportId" serial NOT NULL REFERENCES "Transport"("Id"),
    "TimeStart" timestamp NOT NULL DEFAULT now(),
    "TimeEnd" timestamp,
    "PriceOfUnit" real NOT NULL,
    "PriceType" text NOT NULL,
    "FinalPrice" real
);

