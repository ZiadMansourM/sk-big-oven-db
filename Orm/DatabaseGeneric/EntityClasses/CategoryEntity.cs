﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.9.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Orm.HelperClasses;
using Orm.FactoryClasses;
using Orm.RelationClasses;

using SD.LLBLGen.Pro.ORMSupportClasses;

namespace Orm.EntityClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END

	/// <summary>Entity class which represents the entity 'Category'.<br/><br/></summary>
	[Serializable]
	public partial class CategoryEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END
	
	{
		private EntityCollection<RecipesCategoryEntity> _recipesCategories;
		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END

		private static CategoryEntityStaticMetaData _staticMetaData = new CategoryEntityStaticMetaData();
		private static CategoryRelations _relationsFactory = new CategoryRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name RecipesCategories</summary>
			public static readonly string RecipesCategories = "RecipesCategories";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class CategoryEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public CategoryEntityStaticMetaData()
			{
				SetEntityCoreInfo("CategoryEntity", InheritanceHierarchyType.None, false, (int)Orm.EntityType.CategoryEntity, typeof(CategoryEntity), typeof(CategoryEntityFactory), false);
				AddNavigatorMetaData<CategoryEntity, EntityCollection<RecipesCategoryEntity>>("RecipesCategories", a => a._recipesCategories, (a, b) => a._recipesCategories = b, a => a.RecipesCategories, () => new CategoryRelations().RecipesCategoryEntityUsingCategoryId, typeof(RecipesCategoryEntity), (int)Orm.EntityType.RecipesCategoryEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static CategoryEntity()
		{
		}

		/// <summary> CTor</summary>
		public CategoryEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public CategoryEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this CategoryEntity</param>
		public CategoryEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Category which data should be fetched into this Category object</param>
		public CategoryEntity(System.Guid id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Category which data should be fetched into this Category object</param>
		/// <param name="validator">The custom validator object for this CategoryEntity</param>
		public CategoryEntity(System.Guid id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected CategoryEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'RecipesCategory' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoRecipesCategories() { return CreateRelationInfoForNavigator("RecipesCategories"); }
		
		/// <inheritdoc/>
		protected override EntityStaticMetaDataBase GetEntityStaticMetaData() {	return _staticMetaData; }

		/// <summary>Initializes the class members</summary>
		private void InitClassMembers()
		{
			PerformDependencyInjection();
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassMembers
			// __LLBLGENPRO_USER_CODE_REGION_END

			OnInitClassMembersComplete();
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this CategoryEntity</param>
		/// <param name="fields">Fields of this entity</param>
		private void InitClassEmpty(IValidator validator, IEntityFields2 fields)
		{
			OnInitializing();
			this.Fields = fields ?? CreateFields();
			this.Validator = validator;
			InitClassMembers();
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassEmpty
			// __LLBLGENPRO_USER_CODE_REGION_END


			OnInitialized();
		}

		/// <summary>The relations object holding all relations of this entity with other entity classes.</summary>
		public static CategoryRelations Relations { get { return _relationsFactory; } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'RecipesCategory' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathRecipesCategories { get { return _staticMetaData.GetPrefetchPathElement("RecipesCategories", CommonEntityBase.CreateEntityCollection<RecipesCategoryEntity>()); } }

		/// <summary>The Id property of the Entity Category<br/><br/></summary>
		/// <remarks>Mapped on  table field: "categories"."id".<br/>Table field type characteristics (type, precision, scale, length): Uuid, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.Guid Id
		{
			get { return (System.Guid)GetValue((int)CategoryFieldIndex.Id, true); }
			set	{ SetValue((int)CategoryFieldIndex.Id, value); }
		}

		/// <summary>The IsActive property of the Entity Category<br/><br/></summary>
		/// <remarks>Mapped on  table field: "categories"."is_active".<br/>Table field type characteristics (type, precision, scale, length): Boolean, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean IsActive
		{
			get { return (System.Boolean)GetValue((int)CategoryFieldIndex.IsActive, true); }
			set	{ SetValue((int)CategoryFieldIndex.IsActive, value); }
		}

		/// <summary>The Name property of the Entity Category<br/><br/></summary>
		/// <remarks>Mapped on  table field: "categories"."name".<br/>Table field type characteristics (type, precision, scale, length): Text, 0, 0, 1073741824.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)CategoryFieldIndex.Name, true); }
			set	{ SetValue((int)CategoryFieldIndex.Name, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'RecipesCategoryEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(RecipesCategoryEntity))]
		public virtual EntityCollection<RecipesCategoryEntity> RecipesCategories { get { return GetOrCreateEntityCollection<RecipesCategoryEntity, RecipesCategoryEntityFactory>("Category", true, false, ref _recipesCategories); } }
		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END


	}
}

namespace Orm
{
	public enum CategoryFieldIndex
	{
		///<summary>Id. </summary>
		Id,
		///<summary>IsActive. </summary>
		IsActive,
		///<summary>Name. </summary>
		Name,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace Orm.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Category. </summary>
	public partial class CategoryRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between CategoryEntity and RecipesCategoryEntity over the 1:n relation they have, using the relation between the fields: Category.Id - RecipesCategory.CategoryId</summary>
		public virtual IEntityRelation RecipesCategoryEntityUsingCategoryId
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "RecipesCategories", true, new[] { CategoryFields.Id, RecipesCategoryFields.CategoryId }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticCategoryRelations
	{
		internal static readonly IEntityRelation RecipesCategoryEntityUsingCategoryIdStatic = new CategoryRelations().RecipesCategoryEntityUsingCategoryId;

		/// <summary>CTor</summary>
		static StaticCategoryRelations() { }
	}
}
