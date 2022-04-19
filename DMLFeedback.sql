
INSERT INTO FeedbackCategory(FeedbackCategoryId,FeedbackCategoryName) VALUES ('CFD428CC-988E-422A-8E7F-F16D360B3C29','warning');
INSERT INTO FeedbackCategory(FeedbackCategoryId,FeedbackCategoryName) VALUES ('6A1AC8DA-8C9F-413D-8581-A1E05D745BFD','critical');
INSERT INTO FeedbackCategory(FeedbackCategoryId,FeedbackCategoryName) VALUES ('8CB0BE72-E0F9-4B12-912D-BCBB40E70966','informational');
INSERT INTO FeedbackCategory(FeedbackCategoryId,FeedbackCategoryName) VALUES ('645A9B79-1AF3-4D54-B3AA-B0901C3CA3D6','incoming');
INSERT INTO FeedbackCategory(FeedbackCategoryId,FeedbackCategoryName) VALUES ('80345C31-E3C9-4549-85BA-500D41D4444C','risk');

INSERT INTO Feedback(FeedbackId,FeedbackCategoryId,ObjectStoreCheckId,[Text],[Date],Resolved,Img,Username) VALUES ('CFD428CC-988E-422A-8E7F-F16D360B3C29','CFD428CC-988E-422A-8E7F-F16D360B3C29','23506C9F-E66E-4A52-B5A8-8E7D2AED8B26','text1','2023-03-03',1,'a','b');
INSERT INTO Feedback(FeedbackId,FeedbackCategoryId,ObjectStoreCheckId,[Text],[Date],Resolved,Img,Username) VALUES ('6A1AC8DA-8C9F-413D-8581-A1E05D745BFD','6A1AC8DA-8C9F-413D-8581-A1E05D745BFD','24D585A9-F6BB-4EA9-8191-18E05E2BCB15','text2','2023-03-04',1,'c','d');
INSERT INTO Feedback(FeedbackId,FeedbackCategoryId,ObjectStoreCheckId,[Text],[Date],Resolved,Img,Username) VALUES ('8CB0BE72-E0F9-4B12-912D-BCBB40E70966','8CB0BE72-E0F9-4B12-912D-BCBB40E70966','E2651D74-C864-4362-A93B-D5BF04E4745F','text3','2023-03-05',1,'e','f');
INSERT INTO Feedback(FeedbackId,FeedbackCategoryId,ObjectStoreCheckId,[Text],[Date],Resolved,Img,Username) VALUES ('645A9B79-1AF3-4D54-B3AA-B0901C3CA3D6','645A9B79-1AF3-4D54-B3AA-B0901C3CA3D6','77FE3F12-2938-49D4-98D9-9B71D8201C49','text4','2023-03-06',0,'g','h');
INSERT INTO Feedback(FeedbackId,FeedbackCategoryId,ObjectStoreCheckId,[Text],[Date],Resolved,Img,Username) VALUES ('80345C31-E3C9-4549-85BA-500D41D4444C','80345C31-E3C9-4549-85BA-500D41D4444C','12C3EF9C-618D-4A6B-ACF5-65917FCB18CF','text5','2023-03-07',0,'j','k');

select * from feedback