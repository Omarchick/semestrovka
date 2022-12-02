CREATE TABLE public.products
(
    id serial NOT NULL,
    name character varying(50) NOT NULL,
    information text,
    rating real default -1,
    price real not null,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public.products
    OWNER to omr;