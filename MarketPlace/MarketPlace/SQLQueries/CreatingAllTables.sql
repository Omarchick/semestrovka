CREATE TABLE public.products
(
    id serial NOT NULL,
    name character varying(50) NOT NULL,
    information text,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public.products
    OWNER to omr;
CREATE TABLE public.reviews
(
    id serial NOT NULL,
    reviewername character varying NOT NULL,
    rating int NOT NULL,
    message text,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public.reviews
    OWNER to omr;
CREATE TABLE public.userproducts
(
    userid int NOT NULL,
    productname character varying(50) NOT NULL,
    productcount BIGINT,
    PRIMARY KEY (userid)
);

ALTER TABLE IF EXISTS public.userproducts
    OWNER to omr;
CREATE TABLE users
(
    id serial NOT NULL,
    name character varying(50) NOT NULL,
    password character varying(100) NOT NULL,
    balance BIGINT default 0,
    PRIMARY KEY (id)
);