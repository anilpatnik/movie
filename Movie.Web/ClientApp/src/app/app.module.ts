import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastNotificationsModule } from "ngx-toast-notifications";
import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
import { MovieComponent } from "./movies/movie.component";
import { DuplicateComponent } from "./duplicate/duplicate.component";

@NgModule({
  declarations: [AppComponent, NavMenuComponent, HomeComponent, MovieComponent, DuplicateComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    BrowserAnimationsModule,
    ToastNotificationsModule.forRoot({ duration: 5000, type: "danger", position: "top-right" }),
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "movies/:type", component: MovieComponent, pathMatch: "full" },
      { path: "movies/:type/:id", component: MovieComponent },
      { path: "duplicates", component: DuplicateComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
