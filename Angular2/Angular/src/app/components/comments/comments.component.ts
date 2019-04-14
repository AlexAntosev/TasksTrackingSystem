import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { Comment } from 'src/app/models/comment';
import { CommentsService } from 'src/app/services/comments.service';
import { formatDate } from '@angular/common';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

  @Input()
  public currentTaskId: number;

  public comments: Comment[];
  public newCommentDescription: string;

  constructor(private commentsService: CommentsService, private accountService: AccountService) { }

  ngOnInit() {
    
  }

  ngOnChanges(){
    this.commentsService.GetAllCommentsByTaskId(this.currentTaskId)
    .subscribe(
      (comments) => {
        this.comments = comments;
      }
    )
  }

  public createComment(description: string) {
    let comment: any = {
      Description: description,
      AuthorId: this.accountService.getCurrentUser().Id,
      TaskId: this.currentTaskId,
      Time: formatDate(Date.now(), 'yyyy-MM-dd', 'en'),
    }

    this.commentsService.createComment(this.currentTaskId, comment as Comment)
    .subscribe(
      (comment) => {
        this.commentsService.GetAllCommentsByTaskId(this.currentTaskId)
        .subscribe(
          (comments) => {
            this.comments = comments;
          }
        )
      }
    )
  }

}
