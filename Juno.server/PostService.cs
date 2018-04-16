using Juno.Data.Infrastructure;
using Juno.Data.Repositories;
using Juno.Model.Models;
using System.Collections.Generic;

namespace Juno.server
{
    public interface IPostService
    {
        
        void Add(Post post);

        void Delete(int Id);

        void Update(Post post);

        IEnumerable<Post> Getall();
        IEnumerable<Post> GetAllPaging(int page,int pagesize,out int totalrow);
        Post GetById(int id);

        IEnumerable<Post> GetAllByTagPaging(int page, int pagesize, out int totalrow);
        void SaveChanges();

    }

    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        IUnitOfWork _unitOfWork;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }
        public void Add(Post post)
        {
            _postRepository.Add(post);
        }

        public void Delete(int Id)
        {
            _postRepository.Delete(Id);
        }

        public IEnumerable<Post> Getall()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging( int page, int pagesize, out int totalrow)
        {
            return _postRepository.GetMultiPaging(x => x.Status , out totalrow, page, pagesize);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pagesize, out int totalrow)
        {
            //select all post by tag
            return _postRepository.GetMultiPaging(x => x.Status, out totalrow, page, pagesize);
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _unitOfWork.Commit();
        }
    }
}