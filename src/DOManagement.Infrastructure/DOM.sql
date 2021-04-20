create table if not exists patient (
    id integer primary key autoincrement ,
    names text not null ,
    surnames text not null ,   
    age integer not null ,
    birthday date not null ,                  
    enabled boolean default true ,
    created date default current_timestamp,
    created_by text not null ,
    last_modified date null ,
    last_modified_by text null
);

create table if not exists specialist (
    id integer primary key autoincrement ,
    names text not null ,
    surnames text not null ,
    enabled boolean default true ,
    created date default current_timestamp ,
    created_by text not null ,
    last_modified date null ,
    last_modified_by text null                       
);

create table if  not exists appointment (
    id integer primary key autoincrement ,
    date_at date not null ,
    description text null ,
    patient_id integer not null,
    specialist_id integer not null,
    completed boolean default false ,
    enabled boolean default true ,
    created date default current_timestamp ,
    created_by text not null ,
    last_modified date null ,
    last_modified_by text null,
    foreign key (patient_id) references patient (id),
    foreign key (specialist_id) references specialist (id)
);

create table if not exists allergy (
    id integer primary key autoincrement ,
    name text not null,
    enabled boolean default true ,
    created date default current_timestamp,
    created_by text not null ,
    last_modified date null ,
    last_modified_by text null
);

create table if not exists patient_allergies (
    patient_id integer not null,
    allergy_id integer not null,
    created date default current_timestamp,
    created_by text not null ,
    last_modified date null ,
    last_modified_by text null,
    foreign key (patient_id) references patient (id),
    foreign key (allergy_id) references allergy (id),
    unique (patient_id, allergy_id)
);