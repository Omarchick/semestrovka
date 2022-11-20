CREATE TABLE public.products
(
    id serial NOT NULL,
    name character varying(50) NOT NULL,
    information text,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public.products
    OWNER to omr;