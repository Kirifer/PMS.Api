do $$
begin
  -- Create users table
  create table if not exists public.users (
    id uuid not null,
    first_name text not null,
    last_name text not null,
    position text,
    email text not null,
    password text,
    is_supervisor boolean not null,
    is_active boolean not null,
    is_deleted boolean not null,
    its_reference_id uuid,
   
    constraint pk_users primary key (id),
    constraint un_users_email unique(email)
  );

  create index ix_users_id
    on public.users using btree
    (id asc nulls last);
end;
$$