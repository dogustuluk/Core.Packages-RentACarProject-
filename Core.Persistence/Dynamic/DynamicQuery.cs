using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Dynamic;

public class DynamicQuery
{
	public IEnumerable<Sort>? Sort { get; set; }
	public Filter? Filter { get; set; } //tek bir filtre değildir, filter class'ı zincir gibi çalışır.

	public DynamicQuery()
	{

	}

	public DynamicQuery(IEnumerable<Sort>? sort, Filter filter)
	{
		Sort = sort;
		Filter = filter;
	}
}
