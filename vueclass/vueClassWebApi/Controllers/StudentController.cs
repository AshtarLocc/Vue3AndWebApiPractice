using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using vueClassWebApi.Data;

namespace vueClassWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly ISqlSugarClient _sqlSugarClient;

		public StudentController(ISqlSugarClient sqlSugarClient)
		{
			_sqlSugarClient = sqlSugarClient;
		}

		[HttpPost("Add")]
		public async Task<int> Add(Student student)
		{
			if (student == null) return 0;
			return await _sqlSugarClient.Insertable(student).ExecuteCommandAsync();
		}

		[HttpGet("Show")]
		public async Task<List<Student>> Show()
		{
			return await _sqlSugarClient.Queryable<Student>().ToListAsync();
		}

		[HttpGet("Get")]
		public async Task<Student> GetStudent(int id)
		{
			return await _sqlSugarClient.Queryable<Student>().FirstAsync(x => x.Id == id);
		}

		[HttpPut("Update")]
		public async Task<int> Update(Student student)
		{
			if (student == null) return 0;
			return await _sqlSugarClient.Updateable(student).ExecuteCommandAsync();
		}

		[HttpDelete("Delete")]
		public async Task<int> Delete(int? id)
		{
			if (!id.HasValue) return 0;
			return await _sqlSugarClient.Deleteable<Student>(id).ExecuteCommandAsync();
		}

		[HttpGet("Search")]
		public List<Student> SearchStudent(string? s)
		{
			if (string.IsNullOrEmpty(s)) return new List<Student>();
			return _sqlSugarClient.Queryable<Student>().Where(r => r.Name!.Contains(s!)).ToList();
		}
	}
}