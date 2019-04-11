import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Comment } from 'src/app/models/comment';
import { Observable } from 'rxjs/internal/Observable';
import { AccountService } from 'src/app/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  public url: string = "http://localhost:60542/api"

  constructor(private http: HttpClient, private accountService: AccountService) {

  }

  public GetAllCommentsByTaskId(taskId: number): Observable<Comment[]>{
    return this.http.get<Comment[]>(this.url + '/Tasks/' + taskId + '/Comments');
  }

  public createComment(taskId: number, description: string): Observable<Comment>{
    return this.http.post<Comment>(this.url + '/Comments', {Description: description, AuthorId: this.accountService.getCurrentUser().Id, TaskId: taskId, Time: "01/01/2000"});
  }
}
