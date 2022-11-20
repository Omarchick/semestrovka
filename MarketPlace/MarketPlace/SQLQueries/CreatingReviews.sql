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