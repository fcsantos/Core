using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Core.Web.Helpers
{
    public struct FaixaEtaria : IEqualityComparer<FaixaEtaria>
	{
		public int? IdadeMinima { get; set; }
		public int? IdadeMaxima { get; set; }

		public override string ToString()
		{
			if (IdadeMinima.HasValue && IdadeMaxima.HasValue)
				return $"de {IdadeMinima.Value} até {IdadeMaxima} anos";

			if (IdadeMinima.HasValue)
				return $"Acima de {IdadeMinima.Value} anos";

			if (IdadeMaxima.HasValue)
				return $"Até {IdadeMaxima} anos";

			return "";
		}

		public static readonly IEnumerable<FaixaEtaria> FaixaEtarias = new[]
		{
			new FaixaEtaria { IdadeMinima = null, IdadeMaxima = 18 },
			new FaixaEtaria { IdadeMinima = 19, IdadeMaxima = 45 },
			new FaixaEtaria { IdadeMinima = 46, IdadeMaxima = 65 },
			new FaixaEtaria { IdadeMinima = 66, IdadeMaxima = null },
		};

		public static FaixaEtaria Get(int idade)
		{
			return FaixaEtarias.FirstOrDefault(f => f.IdadeMinima.GetValueOrDefault() <= idade && idade <= f.IdadeMaxima.GetValueOrDefault(200));
		}

        public bool Equals([AllowNull] FaixaEtaria x, [AllowNull] FaixaEtaria y)
        {
			return x.GetHashCode() == y.GetHashCode();
        }

        public int GetHashCode([DisallowNull] FaixaEtaria obj)
        {
			return (obj.IdadeMinima.GetValueOrDefault(0) * 101) + (obj.IdadeMaxima.GetValueOrDefault(200) * 100001);
        }
    }
}
