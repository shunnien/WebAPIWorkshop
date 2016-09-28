namespace AttributeRouting.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AttributeRouting.Models.BookAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AttributeRouting.Models.BookAPIContext context)
        {
            context.Authors.AddOrUpdate(new Author[] {
            new Author() { AuthorId = 1, Name = "Chris Anderson" },
            new Author() { AuthorId = 2, Name = "Cole Nussbaumer Knaflic" },
            new Author() { AuthorId = 3, Name = "Jeff Patton " },
            new Author() { AuthorId = 4, Name = "John MacCormick" }
            });

            context.Books.AddOrUpdate(new Book[] {
                new Book() { BookId = 1,  Title= "TED TALKS ���ܪ��O�q", Genre = "���q����",
                    PublishDate = new DateTime(2016, 06, 27), AuthorId = 1, Description =
                    "�A�i�H�Ψ��y�ӧ��ܦۤv�A�]���ܥ@�� TED�ߤ@�x�誩�t�����n�]���q��˪��^", Price = 320 },
                new Book() { BookId = 2, Title = "���z��", Genre = "�g�پ�",
                    PublishDate = new DateTime(2009, 04, 30), AuthorId = 2, Description =
                    "���}80/20�k�h�A��Q�L������ (�̷s�W�q��)", Price = 450 },
                new Book() { BookId = 3, Title = "Google���ת��Ϫ�²���N", Genre = "�t��/²��",
                    PublishDate = new DateTime(2016, 03, 26), AuthorId = 2, Description =
                    "Google�`�ʭ��פ��}�����A�ЧA����Ϫ�B����ܡA�Ҧ��H��ť�A���I", Price = 420 },
                new Book() { BookId = 4, Title = "�ϥΪ̬G�ƹ��", Genre = "�n��u�{",
                    PublishDate = new DateTime(2016, 05, 10), AuthorId = 3, Description =
                    "User Story Mapping", Price = 580 },
                new Book() { BookId = 5, Title = "���ܥ@�ɪ��E�j�t��k", Genre = "�[��/�Ͷ�",
                    PublishDate = new DateTime(2014, 08, 07), AuthorId = 4, Description =
                    "������q���L�Ҥ��઺�̱j����", Price = 360}
            });
        }
    }
}
