CREATE TABLE public.userproducts
(
    userid int NOT NULL,
    productname character varying(50) NOT NULL,
    productcount BIGINT,
    PRIMARY KEY (userid, productcount)
);

ALTER TABLE IF EXISTS public.userproducts
    OWNER to omr;
