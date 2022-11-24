CREATE TABLE public.user_products
(
    user_id int NOT NULL,
    product_id character varying(50) NOT NULL,
    product_count BIGINT,
    PRIMARY KEY (user_id, product_count)
);

ALTER TABLE IF EXISTS public.user_products
    OWNER to omr;