import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute, Router } from "@angular/router";
import { Location } from "@angular/common";
import { Toaster } from "ngx-toast-notifications";
import * as _ from "lodash";
import { IGenres } from "../interfaces/igenres";
import { IMovies } from "../interfaces/imovies";
import { Movie } from "./movie";

@Component({
  selector: "app-movie-component",
  templateUrl: "./movie.component.html"
})
export class MovieComponent {
  genres: IGenres[];
  movies: IMovies[];
  model = new Movie();

  opType: string;
  movieId: number;
  isNew: boolean = false;
  isEdit: boolean = false;
  isDup: boolean = false;

  constructor(
    @Inject("BASE_URL") private baseUrl: string,
    private toaster: Toaster,
    private http: HttpClient,
    private actRoute: ActivatedRoute,
    private router: Router,
    private location: Location
  ) {
    // url params for edit or new
    this.opType = this.actRoute.snapshot.params.type;
    this.movieId = this.actRoute.snapshot.params.id;

    // find the url type
    if (this.opType === "new") {
      this.isNew = true;
    } else if (this.opType === "edt") {
      this.isEdit = true;
    } else {
      this.isDup = true;
    }
  }

  async ngOnInit() {
    await this.getGenres();
    await this.getMovies();
  }

  // get genres from database
  async getGenres() {
    this.http.get<any>(`${this.baseUrl}api/genres`).subscribe(
      result => (this.genres = result),
      error => this.toaster.open(error)
    );
  }

  // get movies from database
  async getMovies() {
    this.http.get<any>(`${this.baseUrl}api/movies`).subscribe(
      result => {
        this.movies = result;
        // load existing movie on edit clicked
        if (!this.isNew) {
          // find the movie with id match
          const movie = _.find(this.movies, o => o.id === Number(this.movieId));
          this.model = new Movie(
            movie.id,
            movie.name,
            movie.code,
            movie.slug,
            movie.genre.id.toString()
          );
        }
      },
      error => this.toaster.open(error)
    );
  }

  // submits form for create new or update existing movie
  onSubmit(form) {
    // url id param
    const id = this.movieId;
    // form data for submit to database
    const movie = form.value;
    const name = movie.name.trim();
    const code = movie.code.trim();
    const slug = movie.slug.trim();
    // remove the movie to be updated from the movies list
    const newMovies = _.filter(this.movies, o => o.id !== Number(this.movieId));
    // validate duplicate code and slug
    if (newMovies.some(x => x.code === code)) {
      // dont allow duplicate code
      this.toaster.open(`Duplicate Code = ${code}`);
    } else if (newMovies.some(x => x.code === slug)) {
      // dont allow duplicate slug
      this.toaster.open(`Duplicate Slug = ${slug}`);
    } else if (this.isDup && newMovies.some(x => x.name === name)) {
      // dont allow duplicate name for the existing movie update
      // new movie can be created with duplicate name
      this.toaster.open(`Duplicate Name = ${name}`);
    } else {
      // formatted data for api
      const model = { name, code, slug, genreId: Number(movie.genre) };
      // check for existing or new movie with id
      if (id) {
        // updates existing movie
        this.onUpdate(id, model);
      } else {
        // creates new movie
        this.onPost(model);
      }
    }
  }

  // creates new movie in database
  onPost(model) {
    this.http.post<any>(`${this.baseUrl}api/movies`, model).subscribe(
      result => {
        this.toaster.open("New movie has been created successfully");
        // navigate to movies page
        this.router.navigate(["/"]);
      },
      error => this.toaster.open(error)
    );
  }

  // updates existing movie in database
  onUpdate(id, model) {
    this.http.put<any>(`${this.baseUrl}api/movies/${id}`, model).subscribe(
      result => {
        this.toaster.open("Movie has been updated successfully");
        // navigate to movies page
        this.router.navigate(["/"]);
      },
      error => this.toaster.open(error)
    );
  }

  // deletes movie from database on Delete button click
  onDelete(id) {
    this.http.delete<any>(`${this.baseUrl}api/movies/${id}`).subscribe(
      result => {
        this.toaster.open("Movie has been deleted successfully");
        // navigate to previous page
        this.location.back();
      },
      error => this.toaster.open(error)
    );
  }
}
