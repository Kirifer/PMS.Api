do $$
begin
  -- Create employees table
  create table if not exists public.performance_reviews (
    id uuid not null,
    name text not null,
    department_type int not null,
    start_year date,
    end_year date,
    start_date date,
    end_date date,
    employee_id uuid,
    supervisor_id uuid,
    is_active bool not null,
    created_on timestamp with time zone not null,
    creator_id uuid,
    updated_on timestamp with time zone,
    updater_id uuid,
    is_deleted bool not null,

    constraint pk_performance_reviews primary key (id)
  );

  create unique index ix_performance_reviews_id
    on public.performance_reviews using btree
    (id asc nulls last);

  -- Create performance_review_goals table
  create table if not exists public.performance_review_goals (
    id uuid not null,
    performance_review_id uuid not null,
    order_no int not null,
    goals text not null,
    weight decimal not null,
    date text  not null,
    measure_4 text,
    measure_3 text,
    measure_2 text,
    measure_1 text,

    constraint pk_performance_review_goals primary key (id),
    constraint fk_performance_review_goals_performance_review_id foreign key (performance_review_id)
      references public.performance_reviews (id) match simple
      on update no action
      on delete cascade
  );

  create unique index if not exists ix_performance_review_goals_id
    on public.performance_review_goals using btree
    (id asc nulls last);

  create index if not exists ix_performance_review_goals_performance_review_id
    on public.performance_review_goals using btree
    (performance_review_id asc nulls last);

  -- Create performance_review_competencies table
  create table if not exists public.performance_review_competencies (
    id uuid not null,
    order_no int not null,
    performance_review_id uuid not null,
    competency_level_id uuid,
    weight decimal,

    constraint pk_performance_review_competencies primary key (id),
    constraint fk_performance_review_competencies_performance_review_id foreign key (performance_review_id)
      references public.performance_reviews (id) match simple
      on update no action
      on delete cascade
  );

  create unique index if not exists ix_performance_review_competencies_id
    on public.performance_review_competencies using btree
    (id asc nulls last);

  create index if not exists ix_performance_review_competencies_performance_review_id
    on public.performance_review_competencies using btree
    (performance_review_id asc nulls last);
end;
$$