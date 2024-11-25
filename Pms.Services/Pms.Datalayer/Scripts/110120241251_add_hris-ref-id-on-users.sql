do $$
begin
  -- alter table add hris-reference-id, password, is_deleted
  alter table if exists public.users add column if not exists its_reference_id uuid;
  alter table if exists public.users add column if not exists password text;
  alter table if exists public.users add column if not exists is_deleted boolean;
  alter table if exists public.users add column if not exists position text;
end;
$$