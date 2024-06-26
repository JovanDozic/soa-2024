export interface Blog {
  id?: string;
  userId: number;
  title: string;
  description: string;
  creationDate: Date;
  status: BlogStatus;
  images: string[];
  netVotes: number;
  ratings?: BlogRating[] | null;
  blogComments?: BlogComment[] | null;
  reports?: BlogReport[] | null;
}



export enum BlogStatus {
  DRAFT = 1,
  PUBLISHED,
  CLOSED,
  ACTIVE,
  FAMOUS,
}

export enum Vote {
  PLUS = 1,
  MINUS = 0,
}

export interface BlogComment {
  userId: number;
  blogId: string;
  comment: string;
  timeCreated: Date;
  timeUpdated: Date;
  username: string;
}

export interface BlogRating {
  userId: number;
  votingDate: Date;
  mark: Vote;
  blogId: string;
}

export function convertBlogStatusToString(status: BlogStatus): string {
  switch (status) {
    case BlogStatus.DRAFT:
      return 'Draft';
    case BlogStatus.PUBLISHED:
      return 'Published';
    case BlogStatus.CLOSED:
      return 'Closed';
    case BlogStatus.ACTIVE:
      return 'Active';
    case BlogStatus.FAMOUS:
      return 'Famous';
    default:
      return 'Unknown';
  }
}

export interface BlogReport {
  blogId: string;
  userId: number;
  username: string;
  reportAuthorId: number;
  timeCommentCreated: Date;
  timeReported: Date;
  reportReason: BlogReportReason;
  isReviewed: boolean;
  comment: string;
  isAccepted?: boolean;
}

export enum BlogReportReason {
  Spam = 1,
  HateSpeech,
  FalseInfo,
  Bullying, 
  Violence
}
