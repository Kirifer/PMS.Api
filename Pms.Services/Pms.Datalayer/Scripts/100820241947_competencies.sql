do $$
begin
  -- Create employees table
  create table if not exists public.competencies (
    id uuid not null,
    competency text,
    level text,
    description text,
    is_active bool not null,
    is_deleted bool not null,

    constraint pk_competencies primary key (id)
  );

  create unique index if not exists ix_competencies_id
    on public.competencies using btree
    (id asc nulls last);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('0f07dca3-853d-48cf-8064-c31f197717c7','Delivering','Executive','Create safety and security strategies and communicate their importance to our business to give customers confidence in our brand. Ensure that direct reports meet safety and security strategies. They champion what excellent service looks like and why it matters, exploring the future needs and expectations of our customers.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('c3bca538-4d0c-47e2-933e-b47731130999','Delivering','Manager','Develop and share safety and security policies and procedures, monitoring performance, driving improvement and making sure that they are adhered to. They think of the customers’ needs as a driving force in everything they do, looking for new ways to enhance service. They encourage customer feedback and take the lead to put things right when there is a problem. They know what needs to be done to achieve the right outcome they use a logical process for evaluating new ideas and think through decisions or recommendations before giving the go ahead.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('30562efa-fca2-430d-a8bf-95ab87823322','Delivering','Supervisor','Know the importance of keeping the workplace safe and secure, they act as a role model when safety is at risk and escalate safety or security concerns that are not in their power to change. They go above and beyond to assist the customer, they empathise, are calm and quick to act when there is a problem. They balance the customers’ needs with what’s best for the business. When dealing with a situation they ensure they have the right information and they review tasks or projects against plans and objectives.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('febcb18e-3c25-4f03-9565-31dadb093ad6','Delivering','Individual Specialist','Take responsibility for safety and security, follow the right standards and offer suggestions for new ways we can be safe and secure. Complete tasks on time and maintain standards of work, even when under pressure. Review work to check for mistakes, asking questions when unsure, have back up plans when appropriate. Build rapport with customers and act quickly clearly and politely.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('cd250d03-ff13-418f-a591-de07f7c99d9f','Delivering','Individual Operator','Follow safety and security rules and procedures, pause a job to avoid putting others at risk if there are safety or security concerns. Give standards of service for happy customers. Listen to instructions and clarify when unsure.',true,false);


  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('8cbff837-0e7c-4dde-bc95-47b57b196be8','Drive','Executive','Achieve results without compromising standards, driving an evolving but ethical and sustainable business culture. Understand their area of business, what and how to achieve results. They inspire others to focus on results and remain resilient when things go wrong. Recognise and inspire people who have delivered extraordinary results.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('67584211-eb3c-408c-9151-c12033b55741','Drive','Manager','Evaluate progress of teams, projects and people and drive changes to get results. Take on new opportunities and match resources to business demands. Ensure a work - life balance is achieved for self and the team. Inspire team members to stay focussed under pressure.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('1d1f1849-2685-4389-bc55-0a30d7e11ce2','Drive','Supervisor','Go beyond job role to achieve results when necessary; investigate and follow up on  problems to ensure the team can deliver and at the end of the project, discuss learnings with team. Set team members achievable but stretching targets, find out what is expected of the team before taking on new tasks. Prioritise work and share work plans with team. Focus on getting things done and seeking out ways to improve things.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('a328e2ac-f367-4b63-b848-20d2feaaf8c9','Drive','Individual Specialist','Deliver on routine work but be proactive in seeking out new projects or challenges. Follow through on commitments, look for solutions to problems and brief others when things aren’t going to plan. Assess how long tasks will take and break them down into smaller steps. Plan and finish work without being told to.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('2c157f19-bb82-4284-a3b3-9f745ae10489','Drive','Individual Operator','Have a positive attitude and know what needs to be done before they start a new task.',true,false);


  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('14aa0145-b484-40d0-ba5e-1b5e5439725e','Open to Opportunity','Executive','Bring about change by looking externally for ways to improve our products and service, spot patterns and trends that others may not see. Bring new ideas to our business model and create the right structure to get the best from people so that people will work together to solve problems. Challenge managers and teams to make change happen. Take calculated risks when working towards a goal and support others when they do the same.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('036099d7-bb65-4532-80cb-5aaed62a00e0','Open to Opportunity','Manager','Spot and react to opportunities or problems and find ways to make a difference to products, services and processes. Encourage others to come up with new ideas and support them in realising ideas. Use judgement to balance risks and benefits and to predict the impact of decisions. Inspire and involve others to make change happen.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('9ea0c6de-a845-4eaf-9344-47489f2a1cbe','Open to Opportunity','Supervisor','Discuss and push for changes to improve work. Encourage others to come up with ideas and be creative and help with their action plans to achieve change. Come up with different ideas and solutions, but make decisions based on impact on business results, working environment and people. Review own and team workload and adapt to changes without missing deadlines.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('b389c9c7-ff6d-43ef-93ef-6a196037913f','Open to Opportunity','Individual Specialist','They can see why we need to change and can adapt processes and plans to tackle problems, find better ways of working and achieve results. Take responsibility and make good decisions for themselves, teams and customers. Try new things and don’t mind being out of their comfort zone, encourage others to be open to change.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('58d259e0-246c-4c1c-9668-547272ba0539','Open to Opportunity','Individual Operator','Keep calm when things are changing quickly, encourage others to do their best. They add something to the team.',true,false);


  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('ece34779-620a-4d0f-a262-978ae92ace72','Business Thinking','Executive','Make strategic plans which fit with long term and global goals and enable their business unit to achieve the vision. Know which relationships can lead to long term business benefits and build a network of contacts based on those relationships. Look at other leading brands to understand the market.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('0836a8a9-0da1-4f23-bca1-cb1632daee4a','Business Thinking','Manager','Anticipate future requirements; come up with long term plans by testing different options. Maintain focus on day to day management but never lose sight of future plans. Know who key stakeholders are and how they affect the business. Know the competition and how different tactics have worked before. Focus on business results.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('3e7b237a-919e-4721-b90d-1c8e7644e972','Business Thinking','Supervisor','Know the consequences of our plans for the future. Do what’s right for the business now but also focus on how it’s going to affect future plans, Think through impact of future strategy on the team to consider issues or opportunities. Make plans for the future using systematic analysis. Understand how the team’s output links to the direction of the dept/company. Make decisions that keep costs low and operations running smoothly.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('0e9ddf97-8397-45e3-9a9f-88f65d687860','Business Thinking','Individual Specialist','Stay up to date with their area of the industry, Know how their actions shape our business and brand. Know about external policies or procedures which could affect us.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('5a36f4c1-7cc4-4193-8e47-4c8d8f551612','Business Thinking','Individual Operator','Make suggestions as if the organisation was their own company. Act in a way which fits where we are going with the business.',true,false);


  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('54469607-5c83-4536-a3e4-00f3edcafb8a','Collaboration','Executive','Connect and align individuals and groups to improve business performance. Use external contacts and stakeholders to bring in fresh thinking. Create an environment where people can share ideas. Work with other leaders to solve complex problems. Get rid of obstacles which prevent teams working together.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('2c27be8e-cfcb-4c2f-92a5-d3cce36671d2','Collaboration','Manager','Build trust by sharing plans and challenges with own team and other depts. Involve others in new areas of work. Use diverse backgrounds to achieve results and encourage team to work across departments.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('9751fb9c-0cd4-410b-9a8f-e30efee8ef11','Collaboration','Supervisor','Encourage own team to cooperate with and consider others’ needs and to appreciate people’s differences. Help/collaborate with others by sharing ideas and experience and build relationships between departments. Create an inclusive environment.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('3ef0fb9a-43b4-420c-865e-87ffb31e2df0','Collaboration','Individual Specialist','Consider others’ needs when making plans and compromise to fit in with the majority. Prioritise contributing to the wider team. Welcome other cultures into team and recognise that people have different needs.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('1621287b-1798-4bfc-aae0-585a0f82a65f','Collaboration','Individual Operator','Respect individual differences. Share their knowledge and help others with their work to benefit the entire team.',true,false);


  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('e8e8fbda-a962-4308-8819-bd1368296dfa','Engaging Others','Executive','Show and encourage trust. Gain support for ideas through emotional and rational persuasion. Encourage healthy debate and ignite enthusiasm for change. Provide their vision for the future to inspire teams to get involved and turn vision to reality. Positive influence on others –they exemplify company values and ethical standards.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('61fa3c72-9326-437c-801b-2441699f8b80','Engaging Others','Manager','Communicate appropriately depending on audience and situation. They admit their mistakes. Facilitate sensitive discussions to resolve conflict or problems and remove barriers. Inspire their teams to strive for more, assign work based on how people work best. Use informal network of contacts to build support for new ideas.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('ebe2850c-a1e5-4ae9-b840-f9532101a152','Engaging Others','Supervisor','Use feelings and logic when convincing others, Listen, question others and show they respect others ideas, opinions and needs. Don’t mind tricky conversations, but manage their emotions and choose words carefully. Walk the talk, stand up and do what’s right for the business, customers and team. Give out work fairly.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('8e9203e3-2334-476d-a1f1-26f8d23df888','Engaging Others','Individual Specialist','Show enthusiasm, create and encourage team spirit. Work with others to overcome challenges, share knowledge in field of expertise. Respect different opinions, be considerate and be able to talk openly and honestly with colleagues. Remain impartial if the team is in conflict.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('0df10eda-3995-4669-a315-63d4e3ba1fc4','Engaging Others','Individual Operator','They get others involved, they communicate clearly and deal with conflict constructively.',true,false);


  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('2174b842-f9c1-432f-958a-4c17fa009531','Providing Direction','Executive','Explain strategic direction to others, and how to get there, allocating responsibility appropriately so everyone knows what they have to achieve and how. Track performance by benchmarking externally. Discuss outcomes of important work or projects – what went well and where to improve. Take time to give feedback and praise people for a job well done.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('233e1bed-d3e4-49dd-a827-8043f5a3398e','Providing Direction','Manager','Explain where the business is going to others, for cascading through teams. Links team goals to company strategy. Take action when results or performance are lower than target and will work to make things better. Seek feedback from others areas of the business. Monitors that performance improvements are being made.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('011e8baa-efca-444b-8a38-1a10c430f7b3','Providing Direction','Supervisor','Explain how department goals fit into the organisation’s success so individuals understand the importance of their contribution. Set challenging but achievable goals, follow up on progress and share results with the team and with other depts. Give feedback that is specific and observed. Take action with performance shortfalls.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('7a09f914-6e67-49c4-96d7-07259d525b16','Providing Direction','Individual Specialist','Can explain importance of meeting dept goals to others and knows how they as individuals contribute to the company. Follow up on progress of projects and work, communicating where results may affect others or cause potential delays or problems, give timely and specific feedback on progress. Lend a hand to meet team goals.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('ffff2c18-b729-44f6-a7fe-b1f2caf1d28a','Providing Direction','Individual Operator','Understand why their job is important and let their supervisor know if their work is falling behind.',true,false);


  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('7acd4ca3-98cb-41fc-8142-d45f3a536215','Developing Talent','Executive','Highlight dept’s successes across the business and recognise employees for a job well done that contributes to business success. Manage the talent pipeline to increase performance and engagement and develop potential successors.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('6448075d-d4c5-49ea-b04b-4866640ab69b','Developing Talent','Manager','Motivated and committed to improving themselves and their teams to improve the business. Talent spotters who give people a variety of experiences to develop their skills and talents. Recognise people and celebrate team successes. Create development plans for direct reports to build their skills and improve the business.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('497e6054-5a49-42dd-a2fd-31adea1bb599','Developing Talent','Supervisor','Handle issues and problems by using a coaching approach. Talk to colleagues and team members about their careers, motivate them to keep developing and encourage developmental opportunities. Provide on job opportunities to practice new skills. Know own strengths and development areas and how the way they act affects others.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('b0ca8456-c779-47e9-8c4f-a47405a6b841','Developing Talent','Individual Specialist','Self-aware so they know what they are good at / what needs developing - they seek feedback on their work and make an effort to improve. They reflect on what they have done and learn from experience. Open to opportunities to develop, take part in setting their goals.',true,false);

  insert into public.competencies (id, competency, level, description, is_active, is_deleted)
  values ('2e914ec7-f0b2-4870-8a73-c7c20491f100','Developing Talent','Individual Operator','They show new people how everything is done. They give feedback when someone has done a good job. They accept their mistakes and learn from them.',true,false);
end;
$$