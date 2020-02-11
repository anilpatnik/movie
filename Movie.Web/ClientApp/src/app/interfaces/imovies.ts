import { IGenres } from './igenres';

export interface IMovies {
    id: number,
    name: string,
    slug: string,
    code: string,
    genre: IGenres
}
