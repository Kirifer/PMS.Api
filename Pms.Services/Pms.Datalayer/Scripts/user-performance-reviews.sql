do $$
begin
  -- Create user_performance_reviews table
  create table if not exists public.user_performance_reviews (
    id uuid not null,
    user_id uuid,
    performance_review_id uuid not null,
    calibration_comments text,
    employee_review_date date,
    manager_review_date date,
    created_on timestamp with time zone not null default now(),
    updated_on date default current_date,
    creator_id uuid,
    updater_id uuid,
    
    constraint pk_user_performance_reviews primary key (id),
    constraint fk_user_performance_reviews_user foreign key (user_id)
      references public.users (id) on delete cascade,
    constraint fk_user_performance_reviews_performance_review foreign key (performance_review_id)
      references public.performance_reviews (id) on delete cascade
  );

  -- Create indexes
  create index if not exists ix_user_performance_reviews_id
    on public.user_performance_reviews using btree
    (id asc nulls last);
    
  create index if not exists ix_user_performance_reviews_user_id
    on public.user_performance_reviews using btree
    (user_id asc nulls last);

  create index if not exists ix_user_performance_reviews_performance_review_id
    on public.user_performance_reviews using btree
    (performance_review_id asc nulls last);

  create table if not exists public.user_performance_review_goals (
    id uuid not null,
    user_performance_review_id uuid not null,
    performance_review_goal_id uuid not null,
    value int,
    comment text,
    is_manager boolean not null default false,
    created_on date not null default current_date,
    updated_on date default current_date,
    creator_id uuid,
    updater_id uuid,
    
    constraint pk_user_performance_review_goals primary key (id),
    constraint fk_user_performance_review_goals_user_performance_review foreign key (user_performance_review_id)
      references public.user_performance_reviews (id) on delete cascade,
    constraint fk_user_performance_review_goals_performance_review_goal foreign key (performance_review_goal_id)
      references public.performance_review_goals (id) on delete cascade
  );

  -- Create indexes
  create index if not exists ix_user_performance_review_goals_id
    on public.user_performance_review_goals using btree
    (id asc nulls last);

  create index if not exists ix_user_performance_review_goals_user_performance_review_id
    on public.user_performance_review_goals using btree
    (user_performance_review_id asc nulls last);

  create index if not exists ix_user_performance_review_goals_performance_review_goal_id
    on public.user_performance_review_goals using btree
    (performance_review_goal_id asc nulls last);

  create table if not exists public.user_performance_review_competencies (
    id uuid not null,
    user_performance_review_id uuid not null,
    performance_review_competency_id uuid not null,
    value int,
    comment text,
    is_manager boolean not null default false,
    created_on date not null default current_date,
    updated_on date default current_date,
    creator_id uuid,
    updater_id uuid,
    
    constraint pk_user_performance_review_competencies primary key (id),
    constraint fk_user_performance_review_competencies_user_performance_review foreign key (user_performance_review_id)
      references public.user_performance_reviews (id) on delete cascade,
    constraint fk_user_performance_review_competencies_performance_review_competency foreign key (performance_review_competency_id)
      references public.performance_review_competencies (id) on delete cascade
  );

  -- Create indexes
  create index if not exists ix_user_performance_review_competencies_id
    on public.user_performance_review_competencies using btree
    (id asc nulls last);

  create index if not exists ix_user_performance_review_competencies_user_performance_review_id
    on public.user_performance_review_competencies using btree
    (user_performance_review_id asc nulls last);

  create index if not exists ix_user_performance_review_competencies_performance_review_competency_id
    on public.user_performance_review_competencies using btree
    (performance_review_competency_id asc nulls last);
end;
$$;
