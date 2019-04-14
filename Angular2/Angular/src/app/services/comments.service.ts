import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Comment } from 'src/app/models/comment';
import { Observable } from 'rxjs/internal/Observable';
import { AccountService } from 'src/app/services/account.service';
import { ErrorService } from 'src/app/services/error.service';
import { catchError } from 'rxjs/internal/operators/catchError';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  public url: string = "http://localhost:60542/api"

  constructor(private http: HttpClient, private accountService: AccountService, private errorService: ErrorService) {

  }

  public GetAllCommentsByTaskId(taskId: number): Observable<Comment[]>{
    return this.http.get<Comment[]>(this.url + '/Tasks/' + taskId + '/Comments')
    .pipe(
      catchError(this.errorService.handleError)
    );
  }

  public createComment(taskId: number, comment: Comment): Observable<Comment>{
    return this.http.post<Comment>(this.url + '/Comments', comment)
    .pipe(
      catchError(this.errorService.handleError)
    );
  }
}
