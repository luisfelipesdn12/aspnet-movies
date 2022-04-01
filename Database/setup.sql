create database aspnet_movies;
use aspnet_movies;

create table genre (
    id int not null auto_increment primary key,
    name varchar(255) not null
);

create table director (
    id int not null auto_increment primary key,
    name varchar(255) not null
);

create table movie (
    id int not null auto_increment primary key,
    title varchar(255) not null,
    year int not null,
    director_id int not null,
    genre_id int not null,
    foreign key (director_id) references director(id),
    foreign key (genre_id) references genre(id)
);
