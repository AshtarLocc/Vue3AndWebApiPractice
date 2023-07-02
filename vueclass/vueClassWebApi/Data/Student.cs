using SqlSugar;

namespace vueClassWebApi.Data
{
	/// <summary>
	/// 学生信息表，表名Students。
	/// </summary>
	[SugarTable("Students")]
	public class Student
	{
		//学生ID，主键，int类型自增。
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
		public int Id { get; set; }

		//学生姓名，varchar类型，长度为200
		[SugarColumn(ColumnDataType = "varchar", Length = 200)]
		public string? Name { get; set; }

		//学生年龄
		public int Age { get; set; }

		//学生性别，true表示男，false表示女。
		public bool Sex { get; set; }
	}
}