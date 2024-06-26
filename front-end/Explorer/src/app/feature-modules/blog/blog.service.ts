import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Blog, BlogRating, BlogReport } from './model/blog.model';
import { PagedResults } from 'src/app/shared/model/paged-results.model';
import { BlogComment } from './model/blog.model';
import { environment } from 'src/env/environment';
import { BlogStatus } from './blog.status';
import { AuthService } from 'src/app/infrastructure/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private http: HttpClient, private authService: AuthService) { }

  addBlog(blog: Blog): Observable<Blog> {
    blog.id = "3fb3e3e7-b865-4eb8-9d9e-faa9a62b8463";
    
    return this.http.post<Blog>(environment.apiHost + `blog`, blog);
  }

  publishBlog(blogId: string): Observable<Blog> {
    return this.http.patch<Blog>(environment.apiHost + `blog/publish/` + blogId, {});
  }

  getBlog(blogId: string): Observable<Blog> { // HERE
    return this.http.get<Blog>(environment.apiHost + `blog/get/` + blogId);
  }

  getBlogs(): Observable<PagedResults<Blog>> { // HERE
    console.log("front servis");
    return this.http.get<PagedResults<Blog>>(environment.apiHost + `blog/getAll`);
  }

  getFilteredBlogs(filter: BlogStatus): Observable<Blog[]> {
    return this.http.get<Blog[]>(environment.apiHost + `blog/getFiltered?filter=` + filter);
  }

  getComments(): Observable<PagedResults<BlogComment>> {
    return this.http.get<PagedResults<BlogComment>>(environment.apiHost + `tourist/blogComment/getAll`);
  }

  rateBlog(blogId: string, blogRating: BlogRating): Observable<Blog> {
    console.log(blogRating.blogId);
    console.log(blogRating.userId);
    return this.http.post<Blog>(environment.apiHost + `blog/rate/` + blogId, blogRating);
  }

  updateBlogComment(blogId: string, blogComment: BlogComment): Observable<Blog> {
    return this.http.put<Blog>(environment.apiHost + `blog/updateBlogComment/` + blogId, blogComment);
  }

  leaveBlogComment(blogId: string, blogComment: BlogComment): Observable<Blog> {
    console.log("leave a comment");
    console.log(blogId);
    return this.http.post<Blog>(environment.apiHost + `blog/commentBlog/` + blogId, blogComment);
  }
  
  deleteBlogComment(blogId: string, _blogComment: BlogComment): Observable<Blog> {
    return this.http.put<Blog>(environment.apiHost + `blog/deleteBlogComment/` + blogId, _blogComment);
  }
  deleteBlog(blogId: string): Observable<Blog> {
    console.log("u servisu sam");
    return this.http.delete<Blog>(environment.apiHost + 'blog/delete/'+ blogId);
  }


  reportComment(blogId: string, report: BlogReport): Observable<Blog> {
    return this.http.post<Blog>(environment.apiHost + `blog/reportBlogComment/` + blogId, report);
  }

  didUserReportComment(blogId: string, userId: number, comment: BlogComment): Observable<boolean> {
    const uri = environment.apiHost + `blog/didUserReportComment/` + blogId + `/` + userId;
    return this.http.put<boolean>(uri, comment);
  }

  getReviewedReports(): Observable<BlogReport[]> {
    return this.http.get<BlogReport[]>(environment.apiHost + `blog/getReviewedReports`);
  }

  getUnreviewedReports(): Observable<BlogReport[]> {
    return this.http.get<BlogReport[]>(environment.apiHost + `blog/getUnreviewedReports`);
  }

  reviewReport(blogId: string, isAccepted: boolean, report: BlogReport) {
    return this.http.put(environment.apiHost + `blog/reviewReport/` + blogId + `/` + isAccepted, report);
  }

  deleteReportedComment(blogId: string, report: BlogReport) {
    return this.http.put(environment.apiHost + `blog/deleteReportedBlogComment/` + blogId, report);
  }


}
