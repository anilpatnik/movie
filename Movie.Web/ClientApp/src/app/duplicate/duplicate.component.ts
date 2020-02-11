import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Toaster } from "ngx-toast-notifications";
import * as _ from "lodash";
import { IMovies } from "../interfaces/imovies";

@Component({
  selector: "app-duplicate",
  templateUrl: "./duplicate.component.html"
})
export class DuplicateComponent {
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
        // group by movie name - duplicate movies
        payload = _.chain(payload)
          .groupBy("name")
          .map((v, i) => {
            if (v.length > 1)
              return {
                id: _.first(v).id,
                name: i,
                count: v.length
              };
          })
          .filter(x => x !== undefined)
          .value();
        // payload with temp id
        this.movies = _.map(payload, o => _.extend({ index: index++ }, o));
      },
      error => this.toaster.open(error)
    );
  }
}
