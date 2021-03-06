import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Toaster } from "ngx-toast-notifications";
import * as _ from "lodash";
import { IMovies } from "../interfaces/imovies";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html"
})
export class HomeComponent {
  movies: IMovies[];

  constructor(
    @Inject("BASE_URL") private baseUrl: string,
    private toaster: Toaster,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.getMovies();
  }

  // get movies from database
  getMovies() {
    this.http.get<any>(`${this.baseUrl}api/movies`).subscribe(
      result => {
        let index = 1;
        // initial data
        let payload = result;
        // sorted data
        payload = _.orderBy(payload, ["name"], ["asc"]);
        // payload with temp id
        this.movies = _.map(payload, o => _.extend({ index: index++ }, o));
      },
      error => this.toaster.open(error)
    );
  }

  // deletes movie on X click
  onDelete(id) {
    this.http.delete<any>(`${this.baseUrl}api/movies/${id}`).subscribe(
      result => this.getMovies(),
      error => this.toaster.open(error)
    );
  }
}
